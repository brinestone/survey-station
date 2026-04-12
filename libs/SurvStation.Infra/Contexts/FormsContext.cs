using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using SurvStation.Domain.Models;
using SurvStation.Domain.Models.Forms;

namespace SurvStation.Infra.Contexts;

public class FormsContext<TKey>(DbContextOptions<FormsContext<TKey>> options) : DbContext(options) where TKey : struct, IEquatable<TKey>
{
    public virtual DbSet<FormDefinition<TKey>> Forms { get; set; }
    public virtual DbSet<FormVersion<TKey>> FormVersions { get; set; }
    public virtual DbSet<FormSubmission<TKey>> Submissions { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        #region FormSubmissionResponse
        mb.Entity<FormSubmissionResponse<TKey>>(b =>
        {
            b.ToTable("form_submission_responses");
            ConfigureBase(b);

            b.HasAlternateKey(
                nameof(FormSubmissionResponse<>.SubmissionId),
                nameof(FormSubmissionResponse<>.FormId),
                nameof(FormSubmissionResponse<>.FormVersionId),
                nameof(FormSubmissionResponse<>.FormItemId)
            );

            b.HasOne(e => e.Submission)
            .WithMany(e => e.Responses)
            .HasForeignKey(e => e.SubmissionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(e => e.Form)
            .WithMany()
            .HasForeignKey(e => e.FormId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(e => e.FormVersion)
            .WithMany()
            .HasForeignKey(e => e.FormVersionId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(e => e.FormItem)
            .WithMany()
            .HasForeignKey(e => e.FormVersionId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

            b.Property(e => e.Value)
            .HasColumnType("bytea");
        });
        #endregion

        #region FormSubmission
        mb.Entity<FormSubmission<TKey>>(b =>
        {
            b.ToTable("form_submissions");
            ConfigureBase(b);

            b.HasAlternateKey(e => e.Index);
            b.Property(e => e.Index).ValueGeneratedOnAdd();

            b.HasOne(e => e.FormVersion)
            .WithMany()
            .HasForeignKey(e => e.FormVersionId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

            b.HasOne(e => e.Form)
            .WithMany()
            .HasForeignKey(e => e.FormId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

            b.HasMany(e => e.Responses)
            .WithOne(e => e.Submission)
            .HasForeignKey(e => e.SubmissionId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
        });
        #endregion

        #region FormItem
        mb.Entity<FormItem<TKey>>(b =>
        {
            ConfigureBase(b);
            b.HasOne(e => e.Form)
            .WithMany()
            .HasForeignKey(e => e.FormId)
            .IsRequired();
            b.Property(e => e.Type).IsRequired();
            b.Property(e => e.Config)
            .HasColumnType("JSONB")
            .HasConversion(
                v => JsonSerializer.Serialize(v),
                json => JsonSerializer.Deserialize<Dictionary<string, object>>(json),
                 new ValueComparer<Dictionary<string, object>>(
                    (d1, d2) => d1 != null && d2 != null && d1.SequenceEqual(d2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToDictionary()
            ));
        });
        #endregion
        #region FormVersionItem
        mb.Entity<FormVersionItem<TKey>>(b =>
        {
            ConfigureBase(b);
            b.HasAlternateKey(e => new { e.FormId, e.FormVersionId });
            b.HasOne(e => e.FormItem)
            .WithMany()
            .HasForeignKey(e => e.ItemId)
            .OnDelete(DeleteBehavior.SetNull);
            b.HasOne(e => e.FormVersion)
            .WithMany(e => e.Items)
            .HasForeignKey(e => e.FormVersionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(e => e.Parent)
            .WithMany()
            .HasForeignKey(e => e.ParentId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(e => e.Form)
            .WithMany()
            .HasForeignKey(e => e.FormId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
            b.Property(e => e.Path).IsRequired();
            b.ComplexProperty(e => e.Relevance, bb => bb.ToJson());
            b.Property(e => e.Config)
            .HasColumnType("JSONB")
            .HasConversion(
                v => JsonSerializer.Serialize(v),
                json => JsonSerializer.Deserialize<Dictionary<string, object>>(json),
                new ValueComparer<Dictionary<string, object>>(
                    (d1, d2) => d1 != null && d2 != null && d1.SequenceEqual(d2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToDictionary()
                )
            );
        });
        #endregion
        #region FormDefinition
        mb.Entity<FormDefinition<TKey>>(b =>
        {
            ConfigureBase(b);
            b.HasAlternateKey(e => e.Slug);
            b.Property(e => e.Title).IsRequired();
            b.Property(e => e.Description);
            b.Property(e => e.Logo);
            b.HasMany(e => e.Versions)
            .WithOne(e => e.Form)
            .HasForeignKey(e => e.FormId)
            .IsRequired(false);
        });
        #endregion
        #region FormVersion
        mb.Entity<FormVersion<TKey>>(b =>
        {
            ConfigureBase(b);
            b.Property(e => e.IsCurrent);
            b.HasOne(e => e.Form)
            .WithMany()
            .HasForeignKey(e => e.FormId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
            b.HasMany(e => e.Items)
            .WithOne(e => e.FormVersion)
            .HasForeignKey(e => e.FormVersionId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
        });
        #endregion
    }

    private static void ConfigureBase<TEntity>(EntityTypeBuilder<TEntity> mb)
    where TEntity : BaseEntity<TKey>
    {
        mb.HasKey(e => e.Key);
        mb.Property(e => e.CreatedAt)
        .HasValueGenerator<DateTimeGenerator>()
        .ValueGeneratedOnAdd();
        mb.Property(e => e.UpdatedAt)
        .HasValueGenerator<DateTimeGenerator>()
        .ValueGeneratedOnAddOrUpdate();

    }
}

public class DateTimeGenerator : ValueGenerator<DateTime>
{
    public override bool GeneratesTemporaryValues => true;

    public override DateTime Next(EntityEntry _) => DateTime.Now;
}
// class DictionaryConverter : ValueConverter<Dictionary<string, object>, 