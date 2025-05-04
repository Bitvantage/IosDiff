# IosDiff
A console application for comparing and transforming Cisco IOS style configuration files.

## Features
* Show commands unique to the first or second file
* Show commands common to both files
* Show differences between two files
* Merge commands from both files together
* Generate a configuration patch to transform the first file to the second file

## Installation
Ensure that the [.NET Runtime 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) is installed.
Download a prebuilt binary from the [Releases](https://github.com/yourusername/iosdiff/releases) page.

## Usage
```
IosDiff 0.9.0.0 Copyright 2025 Michael Crino
Compares and transforms IOS configuration files.

Usage: IosDiff <mode> [-c|--no-color] [-r|--raw] [-o|--outputFile <output file>] <first file> <second file>

  first     Show lines unique to the first file
  second    Show lines unique to the second file
  common    Show lines common to both files
  diff      Show differences between the first and second file
  merge     Generates a configuration merge
  patch     Generate commands to transform the first configuration into the second configuration
```

### Examples
Compare two configs producing a colorized diff:
```
IosDiff diff router1.cfg router2.cfg
```

Generate a patch script:
```
IosDiff patch router1.cfg router2.cfg
```
## See Also
### ConfigurationSection
[Bitvantage.Cisco.ConfigurationSection](https://github.com/Bitvantage/Cisco.ConfigurationSection) is a .NET library for parsing and manipulating Cisco configuration files. IosDiff is largely a wrapper around it.

## Disclaimer
This software is provided “as is,” without any warranty, express or implied, including but not limited to the implied warranties of merchantability or fitness for a particular purpose.

## License
Licensed under the GNU General Public License v3.
See [https://www.gnu.org/licenses/gpl-3.0.html](https://www.gnu.org/licenses/gpl-3.0.html) for full terms.
