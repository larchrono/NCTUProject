using UnityEditor;

public class SolutionPostprocessor : AssetPostprocessor
{
    // https://learn.microsoft.com/en-us/visualstudio/gamedev/unity/extensibility/customize-project-files-created-by-vstu
    // https://github.com/MicrosoftDocs/visualstudio-docs/blob/main/gamedev/unity/extensibility/customize-project-files-created-by-vstu.md

    // public static string OnGeneratedSlnSolution(string path, string content)
    // {
    //     return content;
    // }

    public static string OnGeneratedCSProject (string path, string content)
    {
        PatchCSProjectForVSCode();
        return content;

        void PatchCSProjectForVSCode()
        {
            const string tag = "<TargetFrameworkVersion>v4.7.1</TargetFrameworkVersion>";
            int index = content.IndexOf(tag);
            if (index < 0) { return; }
            // @note: as of now, hierarchy is <Project> -> <PropertyGroup> -> this tag;
            //        Unity indents blocks with `  `, double space
            content = content.Insert(index + tag.Length, $"{System.Environment.NewLine}    <TargetFramework>net7.0</TargetFramework>");
        }
    }
}