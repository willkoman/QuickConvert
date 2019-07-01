# QuickConvert
 Quickly Converts an image or directory of images to a certain type, quality, and/or size
 
 Essential for graphic designers or users who work with large amounts of images

# Getting Started
### If you haven't already, please make sure you have .NET framework 4.7.2 installed <a href="https://dotnet.microsoft.com/download/thank-you/net472" target="_blank">here</a>

### QuickConvert can be opened with either Powershell or Command Prompt without any real issue. 
### If for some reason you do have any errors, please create an issue ticket, and I'll address it as quickly as I can.


# Usage
```PowerShell
./QuickConvert <sourceimage/directory> <OutputType> <OutputDirectory> [Optional: -s Target Size] [Optional: -q Quality]
```
### OutputType can be one of the following: png, jpg, gif, bmp, tif, ico

#### Note: QuickConvert can automatically detect whether you provide a directory or image and know whether to do a batch job or individual job
