# QuickConvert - Image Converter
Quickly Converts an image or directory of images to a certain type, quality, and/or size
 
Essential for graphic designers or users who work with large amounts of images

# Getting Started
### If you haven't already, please make sure you have .NET framework 4.7.2 installed <a href="https://dotnet.microsoft.com/download/thank-you/net472" target="_blank">here</a>

### QuickConvert can be opened with either Powershell or Command Prompt without any real issue. 
### If for some reason you do have any errors, please create an issue ticket, and I'll address it as quickly as I can.

# Quick Usage
### To Quickly resize any image into whatever resolution you'd like, all you have to do is to name the exe file your target resolution seperated by commas. For instance, 300,200 would resize any image you drop on it into that exact resolution


# Full/Batch Usage
```PowerShell
QuickConvert <sourceimage/directory> <OutputType(png,jpg,gif,bmp,tif,ico)> <OutputDirectory> 
[Optional: -s Target Size (length,width)] No Space Between Comma 
[Optional -i Interpolation Mode] : None - 0, Bicubic - 7, Bilinear - 6, Nearest Neighbor - 5 
[Optional: -q Quality] 0-100
```

### OutputType can be one of the following: png, jpg, gif, bmp, tif, ico
## Do not put a space between the comma in length,width or your resize will not properly work
#### Note: QuickConvert can automatically detect whether you provide a directory or image and know whether to do a batch job or individual job but if you're performing a directory job, please allow only image files in the path.
