# Extract RDL Images
In SSRS reports it is possible to embed images that can be used within the report. However once
embedded, it is not possible to extract the images from Report Builder. This tool consist
of a single binary of 30 kB that is able to extract embedded images from SSRS `.rdl` report files.

## Download
**[Download latest version](https://github.com/dmester/extract-rdl-images/releases/download/1.0.0/ExtractRdlImages.exe)**

Requires .NET Framework 4.0 or later

## Usage
The tool can be operated in the following ways:

* Execute the tool and drag the `.rdl` files onto the tool main window.
* Drag a `.rdl` file onto the tool executable.
* Open the tool and click the *Install* button. Now you can right click the `.rdl` file
  and select *Extract embedded images* from the context menu. 
  
No matter what way you choose the tool will create a subdirectory with the same name as the 
report file, where the extracted images will be placed.

## Uninstall the tool
If you choose to install the tool, it can be uninstalled by either execute the tool and click
the *Uninstall* button, or by using Windows Control Panel.
