# UCSM

Browse all the best albums, all day long!

## Run development

### Start dotnet watcher
`dotnet watch run`

### Build Stylesheets
`npx tailwindcss -i ./wwwroot/css/input.css -o ./wwwwroot/css/site.css --watch`

## Deploy

Push changes to `deploy_to_azure` to trigger a build and deploy on GitHub.