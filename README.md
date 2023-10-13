# Telerik-Blazor-UI-FileManager-Api-Data-Binding
Using Blazor Server and WebAssembly with an API to bind data to the Telerik FileManager component

## Background
I am attempting to apply the Telerik Blazor-UI FileManager "Flat data" demo, text, to a Blazor Server and a standalone WebAssembly application with a Web API using the .NET 7 templates.  To compare apples to apples, I have created a razor class library so the code is identical between hosting models starting at MainLayout.

## Primary Issue
The FileManager works great in Blazor Server, but does not show the folder contents in the BreadCrumb pane using Blazor WebAssembly. The only difference I have found is in the deserialized JSON response. When inspecting the locale Data variable (a List of FlatFileEntry), there is a sublevel of items when using Webassembly.  Whereas Blazor Server gives you the list at the top level without having to drop down into items.  I get the same result using Newtonsoft JSON for deseriialization. See code block and attached images.

Telerik Technical Support says it is a deserilaization issue and "not a component-related issue."  What is causing the difference in behavior?  Will this impact the resulting list object that is passed to components?  And how do you get the same list structure?

### Secondary Issue
I have also notice the View option is not available in WebAssembly when inspecting the local responseBody variable so I can't use the JSON Visualizer.  Is this inherent in using 2022 Community Edition or something else?
