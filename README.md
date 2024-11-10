# photo-manager-tasks

This project is a library that provides custom MSBuild tasks for use in [photo-manager](https://github.com/PABERTHIER/photo-manager) project. These tasks are compiled into a DLL and integrated using the `<UsingTask>` element within MSBuild `.csproj` files.

## Tasks Overview

### 1. `SetFileDates`

The `SetFileDates` task allows you to set specific creation and last modified dates on a list of files. This is useful for ensuring file date consistency during build and testing processes.

**Namespace:** `FileDateTask`

**Properties:**

- `Files` (Required): An array of file paths on which to apply the date.
- `Date` (Required): A string representation of the date to set (e.g., `2024-11-10`).

### 2. `ExtractRarFile`

The `ExtractRarFile` task extracts the contents of a `.rar` file to a specified directory. This task utilises the `SharpCompress` library for extraction.

**Namespace:** `FileExtractionTask`

**Properties:**

- `RarFilePath` (Required): The full path to the `.rar` file to extract.
- `DestinationPath` (Required): The path where the extracted contents will be placed.

## Build Instructions

1. Clone the repository:

   ```bash
   git clone https://github.com/PABERTHIER/photo-manager-tasks.git
   ```

2. Open the solution in your preferred IDE (e.g., Visual Studio or Rider).
3. Build the project to generate the DLL.

## Integration with Other Projects

1. Reference the compiled DLL in the `.csproj` file of the consuming project:

   ```xml
   <UsingTask TaskName="SetFileDates" AssemblyFile="path\to\your\FileDateTask.dll" />
   <UsingTask TaskName="ExtractRarFile" AssemblyFile="path\to\your\ExtractRarFile.dll" />
   ```

2. Add targets in your build process to execute the custom tasks.

    ```xml
    <Target Name="SetFixedDate" AfterTargets="PreBuildEvent">
      <SetFileDates Files="@(TestFiles)" Date="$(FixedDate)" />
    </Target>

    <Target Name="ExtractBinaries" AfterTargets="Build">
      <ExtractRarFile RarFilePath="$(RarFilePath)" DestinationPath="$(DestinationPath)" />
    </Target>
    ```

## Dependencies

- **`SharpCompress`** for `ExtractRarFile` task: Ensure `SharpCompress` is referenced in the project for proper extraction support.

## License

This project is licensed under the MIT License.
