***SEE README.PDF FOR THE CURRENT (JAVA) VERSION!***

Here are the basic instructions for running the program.
  Enter a division name in the top box
  Enter a list of names into the players' box (avoid any extra line breaks)
  Choose a type of bracket from the listbox
  Click Process to create a bracket with the entered names
  This bracket's name will be viewable from the bottom box, along with a count of the players
  Repeat this procedure for more brackets
  Click Output (only once!) to create an output file named "test.txt", created in the same directory where the .exe file is located

The output file is formatted as such:

  Brackets

  <bracket name>:

  <match number>     <player name (club)> - <player name (club)>
  ...
  ...
  [the double line break denotes a change to the next round]
  ...

  Match Cards
  <player name(club)>     <1st match number>     <Blue/White>
  ...

Known bugs
  Clicking Output twice crashes the program
  Extra line returns are considered players with no names

Features not yet implemented
  Support for Round Robin bracket type
  A system of assigning brackets to multiple mats
  A function to delete a bracket, so that it can be recreated
  Proper output for the brackets and match cards to be printed