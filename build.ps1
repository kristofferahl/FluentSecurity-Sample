properties {
	$product		= 'FluentSecurity.Sample'
	$version		= '1.0.0'
	$configuration	= 'release'
	$useVerbose		= $false

	$rootDir		= '.'
	$sourceDir		= "$rootDir\Source"
	$buildDir		= "$rootDir\Build"
	$artifactsDir	= "$buildDir\Artifacts"
	$artifactsName	= "$product-$version-$configuration" -replace "\.","_"
	$deploymentDir	= ''
	
	$buildNumber	= $null
	
	$setupMessage	= 'Executed Setup!'
	$cleanMessage	= 'Executed Clean!'
	$compileMessage	= 'Executed Compile!'
	$testMessage	= 'Executed Test!'
	$packMessage	= 'Executed Pack!'
	$deployMessage	= 'Executed Deploy!'
}

task Default -depends Compile

task Info {
	write-host "running release build" -fore yellow
	write-host "product:        $product" -fore yellow
	write-host "version:        $version" -fore yellow
	write-host "build version:  $buildversion" -fore yellow
}

task Setup {
	nuget_addsource FluentSecurityNightly http://myget.org/F/fluentsecurity/
	get-childitem .\ -recurse -include "packages.config" | % {
		nuget_exe install $_ -outputdirectory "Source\packages"
	}
	$setupMessage
}

task Clean {
	delete_directory $artifactsDir
	create_directory $artifactsDir
	$cleanMessage
}

task Compile -depends Info, Setup, Clean {
	build_solution "$sourceDir\$product.sln"
	$compileMessage
}

task ? -Description "Help" {
	Write-Documentation
}

#------------------------------------------------------------
# Additional functions
#------------------------------------------------------------

function nuget_addsource($name, $source) {
	try {
		$sources = nuget sources
		$containsSource = $false
		$sources | % {
			if ($_.Contains($name) -eq $true -or $_.Contains($source) -eq $true) {
				$containsSource = $true
			}
		}

		if ($containsSource -eq $false) {
			nuget_exe sources add -name $name -source $source
		} else {
			Write-Host "A nuget source with the specified name or source has already been added."
			$sources | Write-Host
		}
	} catch [System.Management.Automation.CommandNotFoundException] {
		throw 'Nuget.exe is not in your path! Add it to your environment variables.'
	}
}