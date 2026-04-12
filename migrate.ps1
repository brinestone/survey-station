param (
    [switch]$Up = $true,
    [Parameter(Mandatory=$true)]
    [string]$MigrationName
)

Write-Host "Creating new migration: $MigrationName";
Invoke-Expression "dotnet ef migrations add $MigrationName --project libs\SurvStation.Infra -s apps\SurvStation.Api"
dotnet ef database update -p libs/SurvStation.Infra -s apps/SurvStation.Api