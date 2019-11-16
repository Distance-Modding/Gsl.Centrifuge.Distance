param($installPath, $toolsPath, $package)
$project = Get-Project
$msbuild = [Microsoft.Build.Evaluation.ProjectCollection]::GlobalProjectCollection.GetLoadedProjects($project.FullName) | Select-Object -First 1
($msbuild.Xml.PropertyGroups | Select-Object -First 1).AddProperty("AllowUnsafeBlocks", "true")
$project.Save()
$(Get-Item $project.FullName).lastwritetime=get-date