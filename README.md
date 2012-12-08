Steam Disk Saver definition
===========================

This is where Steam Disk Saver looks to determine which files are deletable. `apps.yaml` contains all the data; it categorizes the deletable files for each supported game. `buildjson.rb` converts the YAML to JSON.

Feel free to submit a pull request or issue if you want to contribute!

apps.json structure
===================

Here's a fictious apps.yaml containing only Spacewar, app id 480:

	version: 1
	apps:
		480: # Spacewar
			redist:
				- DirectX\
			nonenglish:
				- language\french.txt
				- language\spanish.txt

If a file name ends with a backslash (like the DirectX entry in the example), it will match that directory and everything in it.

Categories
==========

Deletable files belong to a single category. Categories have requirements for files to be included, listed below.

redist
------
This contains installers and redistibutables, which are run only when the game is first started.

This does not includes Steam install scripts.

intro
-----
This contains intro movies which are played when you launch the game program.

Non-game intro movies (like developer/publisher intro movies) are included with 'other', not here.

nonenglish
----------
This contains files which are only needed if the game is played in a different language than English.

other
-----
This contains files which aren't referenced by the game anywhere.

This can include manuals and readme files, soundtracks, linux/osx binaries, etc., as long as the game doesn't actually use these files at all.

This also includes intro movies which aren't related to the game itself (like developer/publisher logo movies).

Options
=======
Some unnecessary files can't simply be deleted. For example, some games crash if an intro movie is deleted. These options support those cases.

For a file, options are declared by wrapping the file name in an array, and appending the options.

Example:

	480: # Spacewar
		intro:
		  - regularfile.dat
			- - startupmovie.dat
			  - empty

empty
-----
Instead of deleting the file, the file is truncated.
