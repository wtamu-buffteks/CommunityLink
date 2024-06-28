# CommunityLink
CommunityLink has been set as the running name

# Setup:

Ensure you have the latest version of .NET
run dotnet --version to see. If you don't have version 8 get it <a src="dotnet.microsoft.com/en-us/download/dotnet/8.0">here</a>:

dotnet.microsoft.com/en-us/download/dotnet/8.0

If you are making a branch for the first time, you will need to restore the bin files. To do this, you'll need to:

- run git clone <repository-url>
- cd <repository-directory>
- dotnet restore
- dotnet build
- Then you should be ready to run with dotnet run. If this doesn't work, please contact me so we can troubleshoot

To get the most up to date version of the development branch:

- git fetch
- git checkout development
- git pull origin development


# Procedural Guidelines

========================================================
<br>
***Always post a message on discord within the BUFFTEKS PROJECT announcing what feature you're working on.***
<br>

========================================================

This will ensure that people are not working on the same feature at once (which could lead to code conflicts).

We will be following the coding conventions found within the .NET documentation. Links to the guides will be added for further reading.

A changelog has been created. The changelog will be used to document any major changes or changes that can be explained in a sentence. This differs from commit messages in that it is not required for minor code fixes and does not track individual contributors. It will give us a better overall perspective of what our code is doing and how it is evolving.

## Style Guidelines
### *** C# Code conventions: [link to conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)

### *** .NET formatting options: [ link to options](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/dotnet-formatting-options) 

<ol>
<li>Sort system import directives first </li>
<li>Seperate system directive groups</li>
</ol>

>  // dotnet_separate_import_directive_groups
>  <br>using System.Collections.Generic;
>  <br>using System.Threading.Tasks;

>  using Octokit;

### *** C# formating options: [ link to options](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/csharp-formatting-options)

The link will lead to a page that has examples of all of the options regarding each of these sections:
<ul>
<li> New-Line options</li>
<li> Indentation Options</li>
<li> Spacing Options</li>
<li> Wrap Options</li>
</ul>

## Branching Strategy: Branches

[link to GitHub flow documentation](https://docs.github.com/en/get-started/using-github/github-flow)

### 🌿 Main Branch:
This branch will hold all ***stable*** versions.

### 🌿 Development Branch:
This branch will be used for pulling, testing, and developing stable versions.

### 🌿 Feature Branches:
We will create separate branches for the development of individual features.  

Branches should be named in a way that explains what feature or issue is being worked on. For example: `feature/login-page` or `bugfix/issue-123`.

## Branching Strategy: GitHub flow
### Make a Branch:
Make a feature branch to work on

### Make Changes:
When making changes to your branch give descriptive commit messages, so your fellow developers know what you were working on.

### Pull Request:
Create pull request to propose and collaborate on changes to a repository. Include a description of what the proposed new code does (Ex: does is fix something? does it add a feature?). You can add comments to specific lines of the pull request if you want to emphasizes parts of your code to reviewers.

### Testing and Merging:

Once a feature branch is complete and tested, it should be merged into the development branch.

Thorough testing should include following the "happy path" and the "not-happy path." Ex. if you make a form for a phone number test it with valid numbers as well as a sting of letters and symbols.

After thorough testing in the development branch, stable changes can be merged into the main branch.


### Delete your Branch

After merging your pull request, delete the branch on the repository. This indicates that the work on the branch is complete and prevents you or others from accidentally using old branches. You can keep a local copy of the branch for back up purposes. [Link](https://docs.github.com/en/repositories/configuring-branches-and-merges-in-your-repository/managing-branches-in-your-repository/deleting-and-restoring-branches-in-a-pull-request)

# Testing Info

Here I am writing info that could be useful for testing:

## Users

I created 8 Users, one for each possible subtype combination:

Hero
Subtypes: None
Password: Hero123

Spider
Subtypes: Volunteer
Password: Spider123

Overlord
Subtypes: Employee
Password: Overlord123

Danger
Subtypes: Requestor
Password: Danger123

Frank
Subtypes: Volunteer, Employee
Password: Frank123

Junnie
Subtypes: Volunteer, Requestor
Password: Junnie123

Ninja
Subtypes: Employee, Requestor
Password: Ninja123

Yea-Yea
Subtypes: Volunteer, Employee, Requestor
Password: Yea-Yea123
