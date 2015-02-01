# Nwire
Nwire - .NET projects synchronization

1. About
============
Nwire is a project that allows to synchronize NuGet packages across multiple C# projects/solutions to ensure that they all use the same references. It also allows to easily pull all changes from GIT repository, as well as perform GIT clean command on multiple folders. Optionally, you can verify that StyleCop is enabled for all projects and that it is set to break compilation on build if any error occurs.

1.1 Packages verification
------------
To verify that all packages are up to date, Nwire uses packages.config file in each project. It checks for the latest version of given package (found in packages config) and checks if there exists a project that contains older version. If such project is found - it is being listed 

1.2 Pulling latest changes for given projects
------------
If you haven't performed local commits, Nwire will pull all changes from remote server so that source code is most up to date.

1.3 Cleaning folders
------------
If you don't have any uncommitted changes, Nwire can perform "git clean" and "git reset --hard" command for all projects (if there are no uncommitted changes), so that you can ensure that your source code works the same way for other members of your team as for you

1.4 Checking StyleCop presence
------------
Nwire, optionally, ensures that all projects have reference to MSBuild.StyleCop and that StyleCop is enabled as prebuild event, so that your team's code is checked against code rules.

2. Usage
============
Nwire is a console application that you can download it by "right clicking" [here](https://github.com/sobanieca/Nwire/blob/master/Bin/Nwire.zip?raw=true) and choosing "Download as..." option. You can also  compile it by yourself (if you want) basing on the source code provided. After obtaining file, unzip it and copy folder to the root directory where your projects are stored. For instance, if you have following projects:

```
C:\Code\MyAmazingProject\
C:\Code\MyAnotherAmazingProject\
C:\Code\MyNotSoAmazingProject\
```

Then, you should put Nwire folder in C:\Code directory, as Nwire scans through all directories (and subdirectories) where it is located.

3. How does it work
=============
When you press R button, Nwire starts to go through all directories in it's current location and scans for all GIT repositories present (it bases on the .git directory presence), searches for all .sln files, and searches for all .csproj files. Then it performs all selected operations.

4. Contribute
=============
If you found this application useful, but some features are missing for you - do not hesitate to clone the repo, add some features (or fix bugs ;)) and make a pull request. Thanks in advance!
