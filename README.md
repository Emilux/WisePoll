# Welcome to the WisePoll project

## Prerequies 
- NPM
- DOTNET

## Configuration 

First add appsettings.json using the appsetting.example.json
as example, and complete with your smtp and database informations.

After that execute the command dotnet ef update to create the database.

You can now build the project and run it.

## SCSS

You have access to npm command to compile the project SCSS Files to CSS

- npm run build:css to compile the scss to css, minify it and using autoprefixer
- npm run watch:css to watch the modification done to scss files and compile them to css 