<Project Sdk="Microsoft.NET.Sdk.Web">

    <PropertyGroup>
        <TargetFramework>net8.0</TargetFramework>
        <Nullable>enable</Nullable>
        <ImplicitUsings>enable</ImplicitUsings>
    </PropertyGroup>

    <ItemGroup>
      <PackageReference Include="DotNetEnv" Version="3.1.1" />
      <PackageReference Include="Groq" Version="0.0.0-dev.6" />
      <PackageReference Include="itext7" Version="8.0.5" />
      <PackageReference Include="itext7.bouncy-castle-adapter" Version="8.0.5" />
      <PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.0" />
      <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.0">
        <PrivateAssets>all</PrivateAssets>
        <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      </PackageReference>
      <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="9.0.0" />
      <PackageReference Include="Portable.BouncyCastle" Version="1.9.0" />
      <PackageReference Include="Radzen.Blazor" Version="5.2.9" />
    </ItemGroup>

    <ItemGroup>
      <ProjectReference Include="..\UFAR.Classwork.Data\UFAR.Classwork.Data.csproj" />
    </ItemGroup>

</Project>
