apps.json categorizes the deletable files for each supported game.

Here's a fictious example for Spacewar, app id 480:

		{
			"480": { /* Spacewar */
				"redist": [
					"directxsetup.exe",
				],
			},
			/* more games */
		}

Categories
==========

redist
------
This contains installers and redistibutables, which are run only when the game is first started.

This also includes Steam install scripts if the install script only executes the installers.

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

		{
			"480": { /* Spacewar */
				"intro": [
					["startupmovie.dat", "empty"],
				],
			}
		}

empty
-----
Instead of deleting the file, the file is truncated.
