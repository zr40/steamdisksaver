Steam Disk Saver metadata definition
====================================

This is where Steam Disk Saver looks to determine which files are deletable. `apps.yaml` contains all the data; it categorizes the deletable files for each supported game. `buildjson.rb` converts the YAML to JSON.

Feel free to submit a pull request or issue if you want to contribute!

The items in `apps.yaml` are described below.

version
=======

This describes the version of the data format. It's only incremented if a backwards incompatible change is introduced. The current version is 2.

Categories
==========

Categories describe deletion categories shown to the user.

* name: the name of the category. It must fit in this text: _Click to delete all <name>._
* description: describes the kind of files belonging to this category.
* keep\_if: optional, describes cases when you don't want to delete these files. It must fit in this text: _Do not delete these if <keep\_if>_
* benefit: optional, describes the additional benefit of deleting these files, other than disk space. It must fit in this text: _Deleting these will <benefit>_
* default: boolean, describes whether this category is selected by default.

Deletion rules
==============

Apps and engines contain deletion rules belonging to a category.

Example:

    480: # Spacewar
      redist:
        - regularfile.dat
        - [startupmovie.dat, empty]
        - directory\

If a rule ends with \, it will match a directory and all its contents.

If a rule is an array, the first item is the rule and the other items are options. These options exist:

* __contains__: Instead of an exact name match, paths only need to contain the match name.
* __empty__: Instead of deleting the file, the file is truncated.
* __startswith__: Instead of an exact name match, paths only need to start with the match name.

Engines
=======

This section describes deletion rules for game engines. These are used to include deletable files which are often present with this game engine.

The contents are categories containing deletion rules (like with apps), except that string substitution is performed on the rules. All rules containing `<key>` (where `key` matches one of the keys in the game's `engine` object) are replaced with that key's value. If a rule then still contains `<` or `>`, it is ignored.

Apps
====

This section describes deletion rules for apps.

The contents are categories containing deletion rules. One exception is the `engine` key, which includes a game engine (according to the `name` key) with substitution parameters.
