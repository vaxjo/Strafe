# Strafe

Automatic file organizer for TV show video files.

## Motivation

While TheRenamer was clumsy and tricky to use it solved the annoying problem of having heaps of badly named TV show video files in a mish-mash of subdirectories by renaming them and standardizing the file hierarchy.
Sadly, towards the beginning of May, 2018, TheRenamer stopped working (TheTVDB.org changed their system and TheRenamer was, as far as anyone could tell, abandoned).
So I spent an afternoon roughing out a new TV show file manager, and continued to pick at over the next couple of weeks.

## Synopsis

A simple WinForms UI onto which files can be dragged. 
Strafe will try its level best to figure out what they are and where they should go (by parsing the filename itself and then using that information to query the TVMaze API).

## Prerequisites

Built in VS 2017 with .NET v4.6.1 and a couple third-party packages.