[Link back to GitHub Repo](https://github.com/Level-5-Pidgey/RobotNavigationProblem/)
# RobotNavigationProblem

RobotNavigationProblems is an assignment done for my introduction to AI Class as part of my Bachelor of Computer Science here at Swinburne. As per the assignment specifications, the program requires "implement[ation of] tree-based search algorithms in software (from scratch) to search for solutions to the
Robot Navigation problem. Both informed and uninformed methods will be required. You will also need to
do some self-learning to learn several search methods (not covered in the lectures)."

## Installation

Clone the repo or download the repo in a .zip file format. The source code is free to be viewed, and the program has already been built in the /bin/Debug/ folder of the solution.

## Usage

The program can used through a command prompt window. Navigate a command prompt window to the directory of the .exe file "Search.exe" (Stored in /RobotNavigationProblem/bin/Debug/ by default), and use:
```c#
Search <filename> <dfs|bfs|astar|greedybest|uniformcost|bidirectional>
```
The filename is a *relative path* to the file from the Search.exe (so for ease of use, place your .txt files in the same directory as the *Search* executable) and the filetype is not required (as it is appended automatically).

Some example files that I've personally used to test my program have already been included within the folder but you are free to use and add your own to test your mazes.

Example usage:
```c#
Search RobotNav-test bidirectional
```
Will return a bidirectional breadth-first search result for the goals contained within the *RobotNav-test* file. Make sure your files are valid before using them within the program!
