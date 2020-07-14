# imgdanke
A small GUI wrapper utility around [imagemagick](https://imagemagick.org/index.php) and [pingo](https://css-ig.net/pingo) for image optimization.

![image](https://user-images.githubusercontent.com/12800223/87372171-85ba8000-c54c-11ea-9e5f-8af9587151d3.png)

## Table of Contents
 * [Technical Details](#technical-details)
 * [Documentation](#tutorial)
   * [Input/Output Setup](#input-output-setup)
     * [Source Folder Path](#source-folder-path)
     * [Output Folder Path](#output-folder-path)
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
   * [Pingo Settings](#pingo-settings)
     * [-pngpalette](#-pngpalette)
       * [-sb](#-sb)
       * [-sa](#-sa)
     * [-s](#-s)
     * [-strip](#-strip)
   * [Files in Source Folder](#files-in-source-folder)
     * [Output Extension](#output-extension)
     * [File Selection](#file-selection)
   * [Commands](#commands)
     * [Magick Command](#magick-command)
     * [Pingo Command](#pingo-command)

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
## Input Output Setup
### Source Folder Path
Click the button and select a folder with images in it.
<details>
    <summary>Currently supported images are:</summary>

- `.png`
- `.jpg`
- `.jpeg`

</details>

### Output Folder Path
Click the button and select a folder to output to. Choosing the same directory as the Source Folder Path will allow you to replace the existing images. Avoiding replacement can be done via manually editing the command, or changing the Output Extension.

## Preset Settings
Selecting any preset setting other than `Custom Preset` will cause the current selections to be replaced.
Additionally, every preset will also set the following:
- `-s9`
- `-strip`
### No Preset
Selecting this preset will set nothing, other than the previously mentioned settings.
### Custom Preset
Selecting this preset will keep anything you currently have selected, and will save any changes you make, and reload them if you close and reopen the program. This is primarily for advanced usage, when you know what the settings do. You should likely set this before manually editing the commands.
### Grayscale Presets
These presets will discard all color information via `-colorspace Gray`.
<details>
    <summary>Selecting one of these presets will always set the following (in addition to previously mentioned settings):</summary>

- `-dither None`
- `-colorspace Gray`
- `-posterize` (with the value of colors listed in the preset)
- `-normalize`

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
#### 4bpp (16 colors, magick)
<details>
    <summary>Selecting this preset will set the following (in addition to previously mentioned settings):</summary>

- `-dither None`
- `-colorspace sRGB`
- `-colors 16`
- `-posterize 256`
- `-normalize`

</details>

#### 4bpp (16 colors, pingo)
<details>
    <summary>Selecting this preset will set the following (in addition to previously mentioned settings):</summary>

- `-colorspace sRGB`
- `-normalize`
- `-pngpalette=24` (16c option)
- `-sb`

</details>

#### 8bpp (256 colors, magick)
<details>
    <summary>Selecting this preset will set the following (in addition to previously mentioned settings):</summary>

- `-dither None`
- `-colorspace sRGB`
- `-colors 256`
- `-posterize 256`
- `-normalize`

</details>

#### 8bpp (256 colors, pingo)
<details>
    <summary>Selecting this preset will set the following (in addition to previously mentioned settings):</summary>

- `-colorspace sRGB`
- `-normalize`
- `-pngpalette=100` (256c option)
- `-sb`

</details>

### Imagemagick Settings
#### -dither
[Magick Command-line reference](https://www.imagemagick.org/script/command-line-options.php?#dither)
Changes the type of dithering magick will apply in cases where reduction occurs.
The following values are currently possible (via the GUI):
- `-dither None`
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
Normalizes the colors of the images. Ensures blacks are #000000 and such. Do not apply this more than once, especially for color images.

### Pingo Settings
#### -pngpalette
Uses a set number of bins, so the options provided to you are the different bits (and the resulting colors). Applies dithering, and often produces a far better result than using magick for color images with 256 colors or less. Selecting a value will allow you to select the `-sb` or `-sa` options.
*NOTE:* As of pingo v0.99 [rc2 32], there is currently a bug in pingo once reaching the 70-74 value range, in that it produces an image with 208 colors, instead of following the pattern of increasing by 24 every 5 values. 75-79 produces 184 colors and then continues the pattern properly.
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
- `-pngpalette=64` (136c)
- `-pngpalette=69` (160c)
- `-pngpalette=74` (208c)
- `-pngpalette=79` (184c)
- `-pngpalette=84` (208c)
- `-pngpalette=89` (232c)
- `-pngpalette=100` (256c)

</details>

#### -sb
Runs through a more stringent set of tests to produce a better result. This is recommended over `-sa`.
#### -sa
Runs through a more stringent set of tests to produce a better result, but less extensive than `-sb`. 
#### -s
*Losslessly* optimizes `.png` files. `-s0` is the least optimal, and `-s9` is the best. `-sb` technically makes `-s9` and `-strip` redundant.
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

</details>

#### -strip
Strips unnecessary metadata from the file. If you don't need metadata, ensure this is checked to reduce filesize slightly.

### Files in Source Folder
#### Output Extension
This will be the file extension applied to the end of `%2` in the magick command string, and will be the final output.
#### File Selection
The commands will be applied to the selected files. All files are automatically selected by default. The list is refreshed every time you finish a command, or press the "Refresh List" button.

### Commands
The magick command will always happen before the pingo command.
#### Magick Command
[Imagemagick command-line processing tutorial](https://imagemagick.org/script/command-line-processing.php)
It's important to note that the order of arguments does matter.
#### Pingo Command
pingo, unlike magick, does not allow for specifying output at this time (v0.99 [rc2 32]). It modifies the images directly, so ensure a copy is made via setting a separate output folder, or adding a prefix to `%2` in the magick command.

---
Any questions, feel free to contact me or create an issue (which is really helpful for keeping track of things to change or add).
