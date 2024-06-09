# CommunityLink
CommunityLink has been set as the running name

# Setup:

Ensure you have the latest version of .NET
run dotnet --version to see. If you don't have version 8 get it <a src="dotnet.microsoft.com/en-us/download/dotnet/8.0">here</a>:

dotnet.microsoft.com/en-us/download/dotnet/8.0

With that, I got it to work on my secondary device. If you have any problems running and pushing to the repository, please let me know!

# Branching

At the time of writing this, version control guidelines have not been fully set up. Therefore, I have created the initial branches based on the following basic rules:

Main Branch: This branch will hold all stable versions.

Development Branch: This branch will be used for pulling, testing, and developing stable versions.

Feature Branches: We will create separate branches for the development of individual features.

Branches should be named in a way that explains what feature or issue is being worked on. For example: `feature/login-page` or `bugfix/issue-123`.

# Merging

Once a feature branch is complete and tested, it should be merged into the development branch.

After thorough testing in the development branch, stable changes can be merged into the main branch.