namespace SurvStation.Domain.Events;


public abstract class BaseFormEventArgs<TKey> : BaseEventArgs where TKey : struct, IEquatable<TKey>
{
    public TKey FormId { get; set; }
}

#region Args
public class NewFormEventArgs<TKey> : BaseFormEventArgs<TKey> where TKey : struct, IEquatable<TKey>;
public class FormUpdatedEventArgs<TKey> : BaseFormEventArgs<TKey> where TKey : struct, IEquatable<TKey>;
public class FormDeletedEventArgs<TKey> : BaseFormEventArgs<TKey> where TKey : struct, IEquatable<TKey>;
#endregion

#region Delegates
public delegate void NewFormCreated<TKey>(NewFormEventArgs<TKey> args) where TKey : struct, IEquatable<TKey>;
public delegate void FormUpdated<TKey>(FormUpdatedEventArgs<TKey> args) where TKey : struct, IEquatable<TKey>;
public delegate void FormDeleted<TKey>(FormDeletedEventArgs<TKey> args) where TKey : struct, IEquatable<TKey>;
#endregion