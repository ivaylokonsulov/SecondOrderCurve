# SecondOrderCurve

## Description

I created a simple app to calculate and draw a second order curve (quadratic curve) using [XAML](https://learn.microsoft.com/en-us/visualstudio/xaml-tools/xaml-overview?view=vs-2022) and [C#](https://learn.microsoft.com/en-us/dotnet/csharp/).
After doing some tests and research, I decided to use the values from -4 to +3 for X, in order achieve decent visual results. 

## Usage

The first step is to input some values for a,b,c and then press 'Draw a curve' button. In the code behind, input from the TextBoxes is first checked to be a valid number, using TryParse method, and then stored into doubles. As per [C# Documentation](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types) the largest double is approximately 10<sup>324</sup>, therefore whatever numbers above this value will be disregarded, and the maximum available will be used.
After ensuring all input is valid, ClearAndBuildCanvas() method is called, which basically draws and sets the coordinate system. This is done in order to clear all previous polylines, if any. 
Next, scale for Y values is determined by calling the Scaling() method. Using the provided a,b,c doubles, maximum and minimum values for Y are calculated which leads to fixing the scale. Scaling is generally conducted by taking the maximum absolute value, whether it is minimum or maximum, and using width and height of the Canvas. Presently, the Canvas has 300x300 dimensions.
After scaling Y, each point for the polyline is computed and added to the PointsCollection. Scaling for X is done during each iteration.

## Installation

During the development, I used Visual Studio 2022, where I created WPF C# .NET application. 
