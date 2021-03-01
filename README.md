# imgdanke
A small GUI wrapper utility around [imagemagick](https://imagemagick.org/index.php) and [pingo](https://css-ig.net/pingo) for image optimization.
Download the latest release here: [https://github.com/DrWhoCares/imgdanke/releases](https://github.com/DrWhoCares/imgdanke/releases).
Download the latest appropriate release of imagemagick and pingo and ensure they are either on your PATH, or that `magick.exe` and `pingo.exe` are in the same folder (or any subfolder) as `imgdanke.exe`.
You can run imgdanke without pingo if you want, or are unable to run pingo (such as on Linux where it may not be possible). Some options will be disabled.
Then simply run the .exe.

![image](https://user-images.githubusercontent.com/12800223/87372171-85ba8000-c54c-11ea-9e5f-8af9587151d3.png)

## Table of Contents
 * [Technical Details](#technical-details)
 * [Documentation](#tutorial)
   * [File Menu](#file-menu)
     * [Open](#open)
       * [User Config](#user-config)
       * [Source Folder](#source-folder)
       * [Output Folder](#output-folder)
       * [imgdanke Folder](#imgdanke-folder)
     * [Save Current Settings](#save-current-settings)
     * [Edit Valid Input Exts...](#edit-valid-input-exts)
     * [Edit Valid Output Exts...](#edit-valid-output-exts)
     * [Exit](#exit)
   * [Preferences Menu](#preferences-menu)
     * [Output Settings...](#output-settings)
     * [Should Output To New Folder](#should-output-to-new-folder)
     * [Use Source Dir As Output Dir](#use-source-dir-as-output-dir)
     * [Add Tags To Filenames](#add-tags-to-filenames)
     * [Add Tags To New Folder](#add-tags-to-new-folder)
     * [Check For Updates On Startup](#check-for-updates-on-startup)
     * [Disable Failed Check Message](#disable-failed-check-message)
     * [Force Check For Updates](#force-check-for-updates)
   * [Help Menu](#help-menu)
     * [GitHub](#github)
     * [Open Documentation](#open-documentation)
        * [From File](#from-file)
        * [On GitHub](#on-github)
     * [About](#about)
   * [Input/Output Setup](#input-output-setup)
     * [Source Folder Path](#source-folder-path)
     * [Output Folder Path](#output-folder-path)
     * [Delete Originals](#delete-originals)
     * [Replace Originals](#replace-originals)
     * [Maintain Folder Structure](#maintain-folder-structure)
   * [Preset Settings](#preset-settings)
     * [No Preset](#no-preset)
     * [Custom Preset](#custom-preset)
     * [Grayscale Presets](#grayscale-presets)
       * [1bpp (2 colors)](#1bpp-2-colors)
       * [4bpp (16 colors)](#4bpp-16-colors)
       * [8bpp (256 colors)](#8bpp-256-colors)
     * [Color Presets](#color-presets)
       * [4bpp (16 colors)](#4bpp-16-colors-magick)
       * [4bpp (16 colors)](#4bpp-16-colors-pingo)
       * [8bpp (256 colors, magick)](#8bpp-256-colors-magick)
       * [8bpp (256 colors, pingo)](#8bpp-256-colors-pingo)
   * [Imagemagick Settings](#imagemagick-settings)
     * [-dither](#-dither)
     * [-colorspace](#-colorspace)
     * [-colors](#-colors)
     * [-depth](#-depth)
     * [-posterize](#-posterize)
     * [-normalize](#-normalize)
     * [-contrast-stretch](#-contrast-stretch)
     * [-auto-level](#-auto-level)
   * [Pingo Settings](#pingo-settings)
     * [-pngpalette](#-pngpalette)
       * [-nodithering](#-nodithering)
     * [-s](#-s)
     * [-strip](#-strip)
   * [Files in Source Folder](#files-in-source-folder)
     * [Output Extension](#output-extension)
     * [Include Subfolders](#include-subfolders)
     * [Include PSDs](#include-psds)
     * [File Selection](#file-selection)
   * [Commands](#commands)
     * [Magick Command](#magick-command)
     * [Pingo Command](#pingo-command)
     * [Prepend To Output](#prepend-to-output)
     * [Append To Output](#append-to-output)
     * [Filesize Displays](#filesize-displays)
       * [Previous Size](#previous-size)
       * [New Size](#new-size)
       * [Total Savings](#total-savings)
     * [Status Display Message](#status-display-message)
   * [Command Line Arguments](#command-line-arguments)

## imgdanke
### Technical Details
imgdanke is a utility built using C# and the .NET Framework (v4.7.2).

It also makes use of these packages:
- [Newtonsoft.Json v12.0.3](https://www.newtonsoft.com/json)
- [WindowsAPICodePack-Core v1.1.2](https://github.com/aybe/Windows-API-Code-Pack-1.1)
- [WindowsAPICodePack-Shell v1.1.1](https://github.com/aybe/Windows-API-Code-Pack-1.1)
- [Onova v2.6.0](https://github.com/Tyrrrz/Onova) (and its dependencies)

Using Onova, the program will automatically check for new releases on Github and prompt the user to install on startup.
Settings are stored in the config file located next to the .exe (after first startup) called `imgdanke_UserConfig.json`.

# Documentation
## File Menu
### Open
#### User Config
Opens the imgdanke User Config file in the default text editor.
#### Source Folder
Opens the path to the [Source Folder Path](#source-folder-path) in explorer, or nothing if [Source Folder Path](#source-folder-path) is invalid or empty.
#### Output Folder
Opens the path to the [Output Folder Path](#output-folder-path) in explorer, or nothing if [Output Folder Path](#output-folder-path) is invalid or empty.
#### imgdanke Folder
Opens the path to where the imgdanke executable is currently stored in explorer.
### Save Current Settings
Manually does a save of the current settings. Almost entirely redundant, since the config is saved after almost any config value is updated.
### Edit Valid Input Exts
Opens up a new dialog box that allows you to edit the valid file extensions which will be parsed for populating the [Files in Source Folder](#files-in-source-folder) ListBox.
### Edit Valid Output Exts
Opens up a new dialog box that allows you to edit the valid file extensions which will be selectable in the [Output Extension](#output-extension) ComboBox.
### Exit
Closes the program.

## Preferences Menu
### Output Settings
#### Output To New Folder
If checked, the final output of the files will be a new folder in the [Output Folder Path](#output-folder-path).
The name of the folder is determined by the Output Setting's [Folder Name](#folder-name) TextBox. Additionally, if [Should Add Tags To New Output Folder](#should-add-tags-to-new-output-folder) is checked, any selected tags will be added.
[Should Output To New Folder](#should-output-to-new-folder) is a shortcut for this setting.
#### Folder Name
The base name of the newly created folder for the final location of the files to be output to. Default is `_danke`.
#### Tags To Append To Filenames
##### Should Add Tags To Filenames
If checked, any selected tags will be appended to the final output filename(s).
[Add Tags To Filenames](#add-tags-to-filenames) is a shortcut for this setting.
##### Filename Tags Include Preset
If checked, and [Should Add Tags To Filenames](#should-add-tags-to-filenames) is true, *one* of the following tags will be added to the final output filename(s):
- `(NoPreset)`
- `(CustomPreset)`
- `(Gray1bpp)`
- `(Gray4bpp)`
- `(Gray8bpp)`
- `(PingoColor4bpp)`
- `(PingoColor8bpp)`
- `(MagickColor4bpp)`
- `(MagickColor8bpp)`
##### Filename Tags Include magick Settings
If checked, and [Should Add Tags To Filenames](#should-add-tags-to-filenames) is true, the value of the magick options selected will be added to the final output filename(s).
Some of them have prepended values, or a static value, and are listed below:
- [-dither](#-dither) is just the value, e.g. `(None)`
- [-colorspace](#-colorspace) is just the value, e.g. `(Gray)`
- [-colors](#-colors) is `colors` and the value, e.g. `(colors16)`
- [-depth](#-depth) is `depth` and the value, e.g. `(depth2)`
- [-posterize](#-posterize) is `pos` and the value, e.g. `(pos16)`
- [-normalize](#-normalize) is just `(norm)`
- [-contrast-stretch](#-contrast-stretch) is just `(cs)`
- [-auto-level](#-auto-level) is just `(al)`
##### Filename Tags Include pingo Settings
If checked, and [Should Add Tags To Filenames](#should-add-tags-to-filenames) is true, the value of the pingo options selected will be added to the final output filename(s).
Some of them have prepended values, or a static value, and are listed below:
- [-pngpalette](#-pngpalette) is `pngpal` and the value, e.g. `(pngpal24)`
- [-nodithering](#-nodithering) is just `(nodither)`
- [-s](#-s) is `-s` and the value, e.g. `(-s9)`
- [-strip](#-strip) is just `(-strip)`
#### Tags To Append To New Output Folder
##### Should Add Tags To New Output Folder
If checked, any selected tags will be appended to the newly created folder that files will be output to.
[Add Tags To New Folder](#add-tags-to-new-folder) is a shortcut for this setting.
##### Folder Tags Include Preset
##### Folder Tags Include magick Settings
##### Folder Tags Include pingo Settings
### Should Output To New Folder
If checked, the final output of the files will be a new folder in the [Output Folder Path](#output-folder-path).
The name of the folder is determined by the Output Setting's [Folder Name](#folder-name) TextBox. Additionally, if [Should Add Tags To New Output Folder](#should-add-tags-to-new-output-folder) is checked, any selected tags will be added.
This is just a shortcut to the same option in the [Output Settings](#output-settings) dialog box.
### Use Source Dir As Output Dir
If checked, [Output Folder Path](#output-folder-path) will be disabled, and the value of it will be replaced by [Source Folder Path](#source-folder-path).
### Add Tags To Filenames
If checked, [Should Add Tags To Filenames](#should-add-tags-to-filenames) will be set to true. Any tags selected will be appended to the final filename(s).
This is just a shortcut to the same option in the [Output Settings](#output-settings) dialog box.
### Add Tags To New Folder
If checked, [Should Add Tags To New Output Folder](#should-add-tags-to-new-output-folder) will be set to true. Any tags selected will be appended to the newly created folder that files will be output to.
This is just a shortcut to the same option in the [Output Settings](#output-settings) dialog box.
### Check For Updates On Startup
If checked, a check for any updates will be made once, after starting up the program.
### Disable Failed Check Message
If checked, disables the dialog box that appears if the check for new updates fails for any reason.
### Force Check For Updates
Performs a manual check for new updates.

## Help Menu
### GitHub
Opens the link to the imgdanke GitHub page, https://github.com/DrWhoCares/imgdanke in the default web browser.
### Open Documentation
#### From File
Opens the README.md file in the default text editor.
#### On GitHub
Opens the link to the imgdanke README.md file on the GitHub page, https://github.com/DrWhoCares/imgdanke/blob/master/README.md in the default web browser.
### About
Opens a dialog box displaying some basic information about imgdanke.

## Input Output Setup
### Source Folder Path
Click the button and select a folder with images in it and/or a folder that contains subfolders which contain images.
Supported filetypes are restricted by what imagemagick and/or pingo allow, but are defined by the list of [Input Filetypes](#input-filetypes-list).
Supports dragging and dropping a file onto the Source Folder Path's TextBox.
Also supports dragging and dropping file(s) onto the [Files in Source Folder](#files-in-source-folder) ListBox, which will update your Source Folder Path and will additionally select the files.
### Output Folder Path
Click the button and select a folder to output to.
Choosing the same directory as the Source Folder Path will replace the original images, should the input and output be the same.
Avoiding replacement can be done via changing the name with [Prepend To Output](#prepend-to-output), [Append To Output](#append-to-output), or [Append Tags To Filenames](#append-tags-to-filenames).
Or you can simply change the output directory, or use [Output To New Folder](#output-to-new-folder).
### Delete Originals
If this is checked, the original files will be deleted after all processing is done except for the following cases:
- The original file is a .psd file.
- Replace Originals is checked and the original file has the same extension as the final file. In other words, if the file is replaced, it won't be deleted afterwards.
### Replace Originals
If this is checked, the file(s) selected will be replaced with the final file, ignoring the Output Folder Path (which is shown by being disabled when checked).
However, the original file(s) selected will not be replaced if the Output Extension differs from the original extension.
### Maintain Folder Structure
This will only appear if Replace Originals is *not* checked, and Include Subfolders *is* checked.
If this is checked, the files output will appear in the Output Folder Path, while maintaining the subfolder structure (assuming you selected files within subfolders). This means that the subfolder paths will be created in the selected Output Folder Path. Should the Source Folder Path and the Output Folder Path be the same, then a new folder named `_OUTPUT` will be created. Any files selected that are *not* within a subfolder, will still end up in this new `_OUTPUT` folder, just at the root of it.

## Preset Settings
Selecting any preset setting other than `Custom Preset` will cause the current selections to be replaced.
Additionally, every preset will also set the following for the pingo command (this is lossless optimization):
- `-s9`
- `-strip`
### No Preset
Selecting this preset will set nothing, other than the previously mentioned settings. This is lossless.
### Custom Preset
Selecting this preset will keep anything you currently have selected, and will save any changes you make, and reload them if you close and reopen the program. This is primarily for advanced usage, when you know what the settings do. You should likely set this before manually editing the commands.
### Grayscale Presets
These presets will discard all color information via `-colorspace Gray`.
<details>
  <summary>Selecting one of these presets will always set the following (in addition to previously mentioned settings):</summary>

- `-dither None`
- `-colorspace Gray`
- `-posterize` (with the value of colors listed in the preset, not used for 8bpp)
- `-normalize` and `-contrast-stretch 0%x0%` or just `-auto-level` for 4bpp (Used to help make the blackest black color `#000000` and whitest white color `#FFFFFF`)

</details>

#### 1bpp (2 colors)
This sets `-posterize 2` and will produce a 1bpp black and white image.
#### 4bpp (16 colors)
This sets `-posterize 16` and will produce a 4bpp grayscale image.
#### 8bpp (256 colors)
This does not set `-posterize 256` as it is redundant because `-colorspace Gray` will handle it.
### Color Presets
*Please note that the pingo options tend to produce a better result than the magick options for color images 8bpp and lower.*
`-pngpalette` does not work past 256 colors.
#### 4bpp (16 colors, pingo)
<details>
  <summary>Selecting this preset will set the following (in addition to previously mentioned settings):</summary>

- `-colorspace sRGB`
- `-normalize`
- `-pngpalette=24` (16c option)

</details>

#### 8bpp (256 colors, pingo)
<details>
  <summary>Selecting this preset will set the following (in addition to previously mentioned settings):</summary>

- `-colorspace sRGB`
- `-normalize`
- `-pngpalette=100` (256c option)

</details>

#### 4bpp (16 colors, magick)
<details>
  <summary>Selecting this preset will set the following (in addition to previously mentioned settings):</summary>

- `-dither None`
- `-colorspace sRGB`
- `-colors 16`
- `-posterize 256`
- `-normalize`
- `-contrast-stretch 0%x0%`

</details>

#### 8bpp (256 colors, magick)
<details>
  <summary>Selecting this preset will set the following (in addition to previously mentioned settings):</summary>

- `-dither None`
- `-colorspace sRGB`
- `-colors 256`
- `-posterize 256`
- `-normalize`
- `-contrast-stretch 0%x0%`

</details>

### Imagemagick Settings
#### -dither
[Magick Command-line reference](https://www.imagemagick.org/script/command-line-options.php?#dither)
Changes the type of dithering magick will apply in cases where reduction occurs.
The following values are currently possible (via the GUI):
- `-dither None`
- `-dither FloydSteinberg`
- `-dither Riemersma`
#### -colorspace
[Magick Command-line reference](https://www.imagemagick.org/script/command-line-options.php?#colorspace)
Changes the colorspace of the image. It's important that this value is set for grayscale images as it does a number of important things under the hood (of magick).
<details>
  <summary>The following values are currently possible (via the GUI):</summary>

- `-colorspace Gray`
- `-colorspace sRGB`

</details>

#### -colors
[Magick Command-line reference](https://www.imagemagick.org/script/command-line-options.php?#colors)
Limits the amount of colors the image can have by setting the maximum bound to the provided value. The actual value of colors in the final result may be less than the provided amount (when possible), but will never exceed the value provided.
#### -depth
[Magick Command-line reference](https://www.imagemagick.org/script/command-line-options.php?#depth)
Sets the bit-depth of the image. Currently unused by any presets.
#### -posterize
[Magick Command-line reference](https://www.imagemagick.org/script/command-line-options.php?#posterize)
[Magick In-depth documentation/tutorial](https://www.imagemagick.org/Usage/quantize/#posterize)
Maximum value of 256. When `-colorspace Gray` is set, set this value to the amount of colors you want in the final image.
#### -normalize
[Magick Command-line reference](https://www.imagemagick.org/script/command-line-options.php?#normalize)
Normalizes the colors of the images. Ensures blacks are #000000 and such. It's not recommended to apply this more than once, especially for color images. On grayscale images it will likely have no effect if applied twice, but that is not always the case.
#### -contrast-stretch
[Magick Command-line reference](https://imagemagick.org/script/command-line-options.php#contrast-stretch)
Performs a contrast stretch to help ensure blackest black color is #000000 and whitest white color is #FFFFFF. It's not recommended to apply this more than once, especially for color images. On grayscale images it will likely have no effect if applied twice, but that is not always the case.
#### -auto-level
[Magick Command-line reference](https://imagemagick.org/script/command-line-options.php#auto-level)
An alternative method to ensuring blackest black color is #000000 and whitest white color is #FFFFFF. I suggest reading the documentation, but in testing, it seems it produces slightly better results for 4bpp grayscale images.
It's not recommended to apply this more than once, especially for color images. On grayscale images it will likely have no effect if applied twice, but that is not always the case.

### Pingo Settings
#### -pngpalette
Uses a set number of bins, so the options provided to you are the different bits (and the resulting colors). Applies dithering, and often produces a far better result than using magick for color images with 256 colors or less.
*NOTE:* As of pingo v0.99 [rc3 16], the bug with the incorrect values has been fixed.
<details>
  <summary>The following values are currently possible (via the GUI):</summary>

- `-pngpalette=9` (2c)
- `-pngpalette=14` (4c)
- `-pngpalette=19` (8c)
- `-pngpalette=24` (16c)
- `-pngpalette=29` (24c)
- `-pngpalette=34` (32c)
- `-pngpalette=39` (40c)
- `-pngpalette=44` (48c)
- `-pngpalette=49` (64c)
- `-pngpalette=54` (88c)
- `-pngpalette=59` (112c)
- `-pngpalette=64` (124c)
- `-pngpalette=69` (136c)
- `-pngpalette=74` (160c)
- `-pngpalette=79` (184c)
- `-pngpalette=84` (208c)
- `-pngpalette=89` (232c)
- `-pngpalette=100` (256c)

</details>

#### -nodithering
Disables dithering when performing lossy operations. Can potentially produce a smaller, and more accurate result when using `-pngpalette` values of `75` (184c) or greater. More extensive testing needs to be done to find appropriate recommendations. Enabled by default for the 8bpp color, pingo preset.
#### -s
*Losslessly* optimizes `.png` files. `-s0` is the least optimal, and `-s9` is the best general use-case. `-sb` is the best for pure filesize optimization.
`-sa` and `-sb` are experimental brute force options (unoptimized for speed). In most cases, `-sb` will produce the same results as `-s9`, but may take more time.
Overall, `-sb` will sometimes produce better results (never worse), but the overall savings will only be around ~5%.
This should essentially always be set, there's nothing to lose.
<details>
  <summary>The following values are currently possible (via the GUI):</summary>

- `-s0`
- `-s1`
- `-s2`
- `-s3`
- `-s4`
- `-s5`
- `-s6`
- `-s7`
- `-s8`
- `-s9`
- `-sa`
- `-sb`

</details>

#### -strip
Strips unnecessary metadata from the file. If you don't need metadata, ensure this is checked to reduce filesize slightly.

### Files in Source Folder
#### Output Extension
This will be the file extension that the final image will be converted to.
#### Include Subfolders
If checked, any subfolders will be parsed for supported filetypes and included in the list of files with their subpath prepended to their name (to differentiate files with the same name, this does not affect the output name).
#### Include PSDs
**NOTE: This setting requires that the PSD file is saved with the "Maximize Compatibility" setting checked. This saves the rasterized version along with the normal layers.**
If checked, PSD files will also be parsed and included in the list of files. If any PSD files are selected, they will be converted to the Output Extension *before* any processing is done via `magick mogrify "inputFilename.ext[0]" "outputFilename.ouputExt"`. You can freely mix PSD files and normal image files.
The checkbox is just a shortcut to adding or removing `.psd` from the list of valid input extensions.
#### File Selection
The commands will be applied to the selected files. All files are automatically selected by default. The list is refreshed every time you finish a command, or press the "Refresh List" button.

### Commands
The magick command will always happen before the pingo command.
#### Magick Command
[Imagemagick command-line processing tutorial](https://imagemagick.org/script/command-line-processing.php)
Uses mogrify to vastly improve processing speed by making use of multithreading.
It's important to note that the order of arguments does matter, in some cases.
The magick command will be skipped in the following situations:
- The command is empty.
- The command is `mogrify -format {outputExt} -path "%1/__danketmp/" -quality 10 *.{outputExt}` (The default command, same as selecting [No Preset](#no-preset)).
- The command is `mogrify -format {outputExt} -path "%1/__danketmp/" *.{outputExt}` (The default command, same as selecting [No Preset](#no-preset), and manually turn off the pingo `-s*` option).
#### Pingo Command
pingo, unlike magick, does not allow for specifying output at this time (v0.99 [rc4 14]). It modifies the images directly, so ensure a copy is made via setting a separate output folder, or adding a prefix, suffix, or tags to the output filename(s), if doing a lossy operation and you're unsure of the results.
#### Prepend To Output
The string entered here will be prepended to the output filename(s). As expected, any characters which are defined as being illegal in filenames cannot appear in the string.
#### Append To Output
The string entered here will be appended to the output filename(s). As expected, any characters which are defined as being illegal in filenames cannot appear in the string.
#### Filesize Displays
#### Previous Size
Displays the total filesize (not the size on disk) of the files (after any PSD conversion) before either of the commands are performed.
#### New Size
Displays the total filesize (not the size on disk) of the files after the commands are performed.
#### Total Savings
Displays the total amount of savings and the percent saved.
#### Status Display Message
Displays information to the user about issues or the current status of processing. When processing is finished, it will display the total time elapsed via C#'s Stopwatch class (so it may not be 100% accurate).

### Command Line Arguments
Currently two command line arguments are supported. You can pass up to two arguments, with the first being assigned to the SourceFolderPath, and the second being assigned to the OutputFolderPath. This functionality will likely be expanded in the future.

---
Any questions, feel free to contact me or create an issue (which is really helpful for keeping track of things to change or add).
