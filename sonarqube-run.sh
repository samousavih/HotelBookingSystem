dotnet sonarscanner begin /k:"test" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_53dee4de8cb06b3cb7bdac644214852eab3770ec"
dotnet build HotelBookingSystem.sln
dotnet sonarscanner end /d:sonar.token="sqp_53dee4de8cb06b3cb7bdac644214852eab3770ec"