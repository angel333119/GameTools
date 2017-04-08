# README #

See the [project wiki](http://jasonharley2o.com/wiki/doku.php?id=start). This is by no means anything complete, it's more of a playground to reverse engineer file formats with - you still need to be using a hex editor and notes before using GameTools to implement a file loader.

# Main Projects #
* GameTools - Library to handle opening files as a "GTFS" to then use the "GT" methods to read from. Also has some colour and texture conversion.
* GameTools2 - GUI for loading files from a few games, viewing images.
* GameTools3D - Library for viewing 3D models, saving to PNG and Collada.

# GameTools Library #
* Use a GTFS to load a file into memory, or a GTFSSlow to work from the FileStream instead.
* Use GT functions to read the GTFS. It's fairly similar to using a FileStream except for getting the data type you want, flipping the endianness, and faster when using a GTFS and many small reads.
* A Pack is a generic way to describe a file table of a packed file to expand.
* Track and GTFSView are used to see area of a GTFS you've read and skipped over. It can be disabled to speed GT up.

# GameTools2 WIP Supported Formats #
* Hotel Dusk - uncompress text and some image files including animations.
* Last Window - may open some images, but does not work for animations.
* Runaway: The Dream of the Turtle (Nintendo DS) - Sprites
* TimeSplitters 2 (Xbox, Gamecube, PS2) - can load some textures, models and parts of levels.
* Yesterday (Steam/Android) - Images and sprite animations.

# Required Packages #
* Be.Windows.Forms.HexBox
* OpenTK

# To Do #
Other than working on the loaders for the game file formats I need to do the following:
* Check GT-KyleHyde is merged into GameTools2 and remove it.
* Change the output/working directory of GameTools2.
* GameTools library shouldn't require HexBox.