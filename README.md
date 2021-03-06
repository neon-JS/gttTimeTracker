# gtt time tracker [![CodeQL](https://github.com/neon-JS/gttTimeTracker/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/neon-JS/gttTimeTracker/actions/workflows/codeql-analysis.yml)
A simple time tracking tool based on git branches

## Status
Currently WIP and in testing phase by me (neon-js).

## Structure
`gtt` is a CLI tool that intercepts git commands to track times for given tasks.
Relevant commands will be forwarded to git after being handled by `gtt`. 
All necessary information is stored under `.git/gtt` as a JSON file. 
Determination of tasks is done by a simple regex which checks for  
_(slash)(uppercase letters)(dash)(numbers)(slash or dash)_  
which equals the typical branch names generated by bitbucket in combination with jira tickets.

## Commands
These are the available `gtt` commands:

- _checkout_  
  Does a git checkout and stores information about the checkout so that the timespan for each 
  task can be calculated. The task is being determined by the branch name. If a task cannot be 
  determined, the previous task won't be paused and gtt does nothing except for forwarding the 
  checkout to git. 
- _today_  
  Lists all tasks of today, including an accumulation of today's tasks and their total time.
- _task \<TASK>_  
  Lists all times that the given task has been checked out, including an accumulation of all 
  task times.
- _start \[\<TASK>]_  
  Starts or resumes a task. If task is omitted, the last started or checked out task will be 
  'resumed'. Otherwise time tracking for given task will be started.
  Hint: Make sure that there's no active task when starting or resuming a new task!
- _stop_  
  Stops time tracking for current task.
  Hint: Make sure that there's an active task when stopping it.
- _help_  
  Shows this help and help page of git.

Any other commands will be forwarded to git. 

## Plans
- Supporting multiple ways to identify tasks of branches
- Testing, testing, testing...
- Way to cleanup existing tasks (_cleanup \<OLDER-THAN-DAYS>_ etc?)

## License
MIT
