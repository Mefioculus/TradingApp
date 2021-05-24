# How to run it

For using this app firstly you need to compile it.
This project using dotnet core 5, so dotnet sdk should been installed on your machine.

## Install dotnet core sdk

The sequence of actions you need to perform will differ depending on the system which you use.

### Windows

First, download last .Net SDK for your system from [official site](dotnet.microsoft.com/download).
To do this, click on "Download .NET SDK x64" in column ".NET 5.0".
Now dotnet 5 is recommended version of dotnet, but by the time you take this step, the situation may change.

After you click on the link, the download of the installation package will begin and a page with further instruction will open.

### Linux

First, open this [page](docs.microsoft/en-us/dotnet/core/install/linux) and make sure that your distibutive of linux is supported.
Next, you can follow the instruction on this page, or use the following command to the terminal:

```bash
# Download script
wget https://dot.net/v1/dotnet-install.sh

# Install dotnet 5 
./dotnet-install.sh -c 5.0

# Delete script (optional) 
rm dotnet-install.sh
```

## Building App

# How to use it

