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
If you don't have any uncommited changes, Nwire can perform "git clean -xdf" command for all projects, so that you can ensure that your source code works the same way for other members of your team as for you

1.4 Checking StyleCop presence
------------
Nwire, optionally, ensures that all projects have reference to MSBuild.StyleCop and that StyleCop is enabled as prebuild event, so that your team's code is checked against code rules.

2. Usage
============
Nwire is a console application that you can download by clicking here. Optionally, you can compile it by yourself basing on the source code provided. After obtaining Nwire.exe and LibGit2Sharp.dll file, copy them to the root directory where your projects are stored. For instance, if you have following projects:

```
C:\Code\MyAmazingProject\
C:\Code\MyAnotherAmazingProject\
C:\Code\MyNotSoAmazingProject\
```

Then, you should put Nwire.exe and LibGit2Sharp.dll in C:\Code directory, as Nwire scans through all directories (and subdirectories) where it is located.

3. Contribute
=============
If you found this application useful, but some features are missing for you - do not hesitate to clone the repo, add some features (or fix bugs ;)) and make a pull request. Thanks in advance!
