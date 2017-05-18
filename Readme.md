### CSharpBasics

# Project Setup
The project was developed with jetbrains rider under ubuntu.   
I used nuget for dependency management from the user interface of jetbrains rider.
The dependencies are XUnit for unit testing and NSubstitute for mocking.
  
# Other steps taken
I tried to setup the best development environment for C# development and unit testing under ubuntu linux.
Three development environment were tried and I decided to use Jetbrains.
* Jetbrains rider (Beta)
* Microsoft Visual Studio Code
* Monodevelop

# Setup the environment in linux

**dotnet-host** - Microsoft .NET Core 1.0.0 - Host  
 *apt -y install dotnet-host*

**nuget** - Package manager for NuGet repos - executable  
*apt -y install nuget*

**ubuntu-make** - setup your development environment on ubuntu easily  
*apt -y install ubuntu-make && umake ide visual-sudio-code*

**Install mono-ci**  
*sudo apt-key adv --keyserver keyserver.ubuntu.com  
 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF*    
 
*echo "deb http://jenkins.mono-project.com/repo/debian sid main"  
 | sudo tee /etc/apt/sources.list.d/mono-jenkins.list*  
 
*sudo apt-get update*  
*sudo apt-get install mono-snapshot-latest* 

** Setting up nuget 2.8.7 ** 
*https://launchpad.net/ubuntu/+source/nuget/2.8.7+md510+dhx1-1*  

* nuget install System.Linq * 
* nuget install NSubstitute * 

TODO: Install nuget 3 on linux, we had issues there. Some Linq assemblies are not available on 2.8.7.

# Create a new project
*dotnet new*  
*dotnet restore --infer-runtimes*  
*dotnet publish*  

# Build the project
*dotnet build project.json*  
*dotnet run project.json*  

# Description

Implemented a simple path finding algorithm for an entry technical test

TODO:  
Check a more efficient algorithm for the task in place 
 https://en.wikipedia.org/wiki/A*_search_algorithm

### Problem Statement

The local commuter railroad services a number of towns in Kiwiland.  Because of monetary concerns, all of the tracks are 'one-way.'  That is, a route from Kaitaia to Invercargill does not imply the existence of a route from Invercargill to Kaitaia.  In fact, even if both of these routes do happen to exist, they are distinct and are not necessarily the same distance! 
 
The purpose of this problem is to help the railroad provide its customers with information about the routes.  In particular, you will compute the distance along a certain route, the number of different routes between two towns, and the shortest route between two towns. 
 
Input:  A directed graph where a node represents a town and an edge represents a route between two towns.  The weighting of the edge represents the distance between the two towns.  A given route will never appear more than once, and for a given route, the starting and ending town will not be the same town. 
 
Output: For test input 1 through 5, if no such route exists, output 'NO SUCH ROUTE'.  Otherwise, follow the route as given; do not make any extra stops!  For example, the first problem means to start at city A, then travel directly to city B (a distance of 5), then directly to city C (a distance of 4). 

1. The distance of the route A-B-C. 

2. The distance of the route A-D. 

3. The distance of the route A-D-C. 

4. The distance of the route A-E-B-C-D. 

5. The distance of the route A-E-D. 

6. The number of trips starting at C and ending at C with a maximum of 3 stops.  In the sample data below, there are two such trips: C-D-C (2 stops). and C-E-B-C (3 stops). 
7. The number of trips starting at A and ending at C with exactly 4 stops.  In the sample data below, there are three such trips: A to C (via B,C,D); A to C (via D,C,D); and A to C (via D,E,B). 
8. The length of the shortest route (in terms of distance to travel) from A to C. 
9. The length of the shortest route (in terms of distance to travel) from B to B. 
10. The number of different routes from C to C with a distance of less than 30.  In the sample data, the trips are: CDC, CEBC, CEBCDC, CDCEBC, CDEBC, CEBCEBC, CEBCEBCEBC. 
 
#### Test Input: 
For the test input, the towns are named using the first few letters of the alphabet from A to D.  A route between two towns (A to B) with a distance of 5 is represented as AB5. 
Graph: AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7 
Expected Output: 
1. Output #1: 9 

2. Output #2: 5 

3. Output #3: 13 

4. Output #4: 22 

5. Output #5: NO SUCH ROUTE 

6. Output #6: 2 

7. Output #7: 3 

8. Output #8: 9 

9. Output #9: 9 

10. Output #10: 7