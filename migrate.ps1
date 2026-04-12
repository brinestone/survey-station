param (
    [switch]$Up = $true,
    [Parameter(Mandatory=$true)]
    [string]$MigrationName
)

if ($Up) {
Write-Host "Creating new migration: $MigrationName";
Invoke-Expression "dotnet ef migrations add $MigrationName --project libs\SurvStation.Infra -s apps\SurvStation.Api";
dotnet ef database update -p libs/SurvStation.Infra -s apps/SurvStation.Api
} else {
    Write-Host "Dropping migration: $MigrationName";
    Invoke-Expression "dotnet ef database update -p libs/SurvStation.Infra -s apps/SurvStation.Api";
}

function Backup-Database {
    # perform database backup here
}