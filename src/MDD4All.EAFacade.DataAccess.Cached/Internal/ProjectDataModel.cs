using System;
using MDD4All.EAFacade.DataModels.Contracts;
using EAAPI = EA;

namespace MDD4All.EAFacade.DataAccess.Cached.Internal
{
    internal class ProjectDataModel : Project
    {
        private readonly EAAPI.Project _apiProject;

        public ProjectDataModel(EAAPI.Project apiProject)
        {
            _apiProject = apiProject;
        }

        public ObjectType ObjectType
        {
            get
            {
                return ObjectType.otProject;
            }
        }

        public void CancelValidation()
        {
            _apiProject.CancelValidation();
        }

        public bool CanValidate()
        {
            bool result = _apiProject.CanValidate();

            return result;
        }

        public bool CreateBaseline(string PackageGUID, string Version, string Notes)
        {
            bool result = _apiProject.CreateBaseline(PackageGUID, Version, Notes);

            return result;
        }

        public bool CreateBaselineEx(string PackageGUID, string Version, string Notes, int Flags)
        {
            bool result = _apiProject.CreateBaselineEx(PackageGUID, Version, Notes, Flags);

            return result;
        }

        public string DefineRule(string CategoryID, EnumMVErrorType Severity, string ErrorMsg)
        {
            string result = _apiProject.DefineRule(CategoryID, (EAAPI.EnumMVErrorType)Severity, ErrorMsg);

            return result;
        }

        public string DefineRuleCategory(string Description)
        {
            string result = _apiProject.DefineRuleCategory(Description);

            return result;
        }

        public bool DeleteBaseline(string Baseline)
        {
            bool result = _apiProject.DeleteBaseline(Baseline);

            return result;
        }

        public string DoBaselineCompare(string PackageGUID, string Baseline, string ConnectString)
        {
            string result = _apiProject.DoBaselineCompare(PackageGUID, Baseline, ConnectString);

            return result;
        }

        public string DoBaselineMerge(string PackageGUID, string Baseline, string MergeInstructions, string ConnectString)
        {
            string result = _apiProject.DoBaselineMerge(PackageGUID, Baseline, MergeInstructions, ConnectString);

            return result;
        }

        public string DoPackageCompareAndMerge(string PackageGUID, string XMIFile, string XMIComparisonFile)
        {
            string result = _apiProject.DoPackageCompareAndMerge(PackageGUID, XMIFile, XMIComparisonFile);

            return result;
        }

        public string EnumDiagramElements(string DiagramGUID)
        {
            string result = _apiProject.EnumDiagramElements(DiagramGUID);

            return result;
        }

        public string EnumDiagramLinks(string DiagramID)
        {
            string result = _apiProject.EnumDiagramLinks(DiagramID);

            return result;
        }

        public string EnumDiagrams(string PackageGUID)
        {
            string result = _apiProject.EnumDiagrams(PackageGUID);

            return result;
        }

        public string EnumElements(string PackageGUID)
        {
            string result = _apiProject.EnumElements(PackageGUID);

            return result;
        }

        public string EnumLinks(string PackageID)
        {
            string result = _apiProject.EnumLinks(PackageID);

            return result;
        }

        public string EnumPackages(string PackageGUID)
        {
            string result = _apiProject.EnumPackages(PackageGUID);

            return result;
        }

        public string EnumProjects()
        {
            string result = _apiProject.EnumProjects();

            return result;
        }

        public string EnumViewEx(string ProjectGUID)
        {
            string result = _apiProject.EnumViewEx(ProjectGUID);

            return result;
        }

        public string EnumViews()
        {
            string result = _apiProject.EnumViews();

            return result;
        }

        public void Exit()
        {
            _apiProject.Exit();
        }

        public string ExportPackageXMI(string PackageGUID, EnumXMIType XMIType, int DiagramXML, int DiagramImage, int FormatXML, int UseDTD, string FileName)
        {
            string result = _apiProject.ExportPackageXMI(PackageGUID, (EAAPI.EnumXMIType)XMIType, DiagramXML, DiagramImage, FormatXML, UseDTD, FileName);

            return result;
        }

        public string ExportPackageXMIEx(string PackageGUID, EnumXMIType XMIType, int DiagramXML, int DiagramImage, int FormatXML, int UseDTD, string FileName, int Flags)
        {
            string result = _apiProject.ExportPackageXMIEx(PackageGUID, (EAAPI.EnumXMIType)XMIType, DiagramXML, DiagramImage, FormatXML, UseDTD, FileName, Flags);

            return result;
        }

        public bool GenerateClass(string ElementGUID, string ExtraOptions)
        {
            bool result = _apiProject.GenerateClass(ElementGUID, ExtraOptions);

            return result;
        }

        public bool GenerateDiagramFromScenario(string ElementGUID, EnumScenarioDiagramType DiagramType, int Options)
        {
            bool result = _apiProject.GenerateDiagramFromScenario(ElementGUID, (EAAPI.EnumScenarioDiagramType)DiagramType, Options);

            return result;
        }

        public bool GenerateElementDDL(string ElementGUID, string GenDDLFilePath, string ExtraOptions)
        {
            bool result = _apiProject.GenerateElementDDL(ElementGUID, GenDDLFilePath, ExtraOptions);

            return result;
        }

        public bool GeneratePackage(string PackageGUID, string ExtraOptions)
        {
            bool result = _apiProject.GeneratePackage(PackageGUID, ExtraOptions);

            return result;
        }

        public bool GeneratePackageDDL(string PackageGUID, string GenDDLFilePath, string ExtraOptions)
        {
            bool result = _apiProject.GeneratePackageDDL(PackageGUID, GenDDLFilePath, ExtraOptions);

            return result;
        }

        public bool GenerateTestFromScenario(string ElementGUID, EnumScenarioTestType TestType)
        {
            bool result = _apiProject.GenerateTestFromScenario(ElementGUID, (EAAPI.EnumScenarioTestType)TestType);

            return result;
        }

        public bool GenerateWSDL(string WSDLComponentGUID, string FileName, string Encoding, string ExtraOptions)
        {
            bool result = _apiProject.GenerateWSDL(WSDLComponentGUID, FileName, Encoding, ExtraOptions);

            return result;
        }

        public bool GenerateXSD(string PackageGUID, string FileName, string Encoding, string Options)
        {
            bool result = _apiProject.GenerateXSD(PackageGUID, FileName, Encoding, Options);

            return result;
        }

        public bool GetAllDiagramImageAndMap(string Directory)
        {
            bool result = _apiProject.GetAllDiagramImageAndMap(Directory);

            return result;
        }

        public string GetBaselines(string PackageGUID, string ConnectString)
        {
            string result = _apiProject.GetBaselines(PackageGUID, ConnectString);

            return result;
        }

        public string GetDiagram(string DiagramGUID)
        {
            string result = _apiProject.GetDiagram(DiagramGUID);

            return result;
        }

        public bool GetDiagramImageAndMap(string DiagramGUID, string Directory)
        {
            bool result = _apiProject.GetDiagramImageAndMap(DiagramGUID, Directory);

            return result;
        }

        public string GetElement(string ElementGUID)
        {
            string result = _apiProject.GetElement(ElementGUID);

            return result;
        }

        public string GetElementConstraints(string ElementGUID)
        {
            string result = _apiProject.GetElementConstraints(ElementGUID);

            return result;
        }

        public string GetElementEffort(string ElementGUID)
        {
            string result = _apiProject.GetElementEffort(ElementGUID);

            return result;
        }

        public string GetElementFiles(string ElementGUID)
        {
            string result = _apiProject.GetElementFiles(ElementGUID);

            return result;
        }

        public string GetElementMetrics(string ElementGUID)
        {
            string result = _apiProject.GetElementMetrics(ElementGUID);

            return result;
        }

        public string GetElementProblems(string ElementGUID)
        {
            string result = _apiProject.GetElementProblems(ElementGUID);

            return result;
        }

        public string GetElementProperties(string ElementGUID)
        {
            string result = _apiProject.GetElementProperties(ElementGUID);

            return result;
        }

        public string GetElementRequirements(string ElementGUID)
        {
            string result = _apiProject.GetElementRequirements(ElementGUID);

            return result;
        }

        public string GetElementResources(string ElementGUID)
        {
            string result = _apiProject.GetElementResources(ElementGUID);

            return result;
        }

        public string GetElementRisks(string ElementGUID)
        {
            string result = _apiProject.GetElementRisks(ElementGUID);

            return result;
        }

        public string GetElementScenarios(string ElementGUID)
        {
            string result = _apiProject.GetElementScenarios(ElementGUID);

            return result;
        }

        public string GetElementTests(string ElementGUID)
        {
            string result = _apiProject.GetElementTests(ElementGUID);

            return result;
        }

        public string GetFileNameDialog(string sName, string filter, int DefaultFilterIndex, int Flags, string defaultDir, EnumFilenameDialog Type)
        {
            string result = _apiProject.GetFileNameDialog(sName, filter, DefaultFilterIndex, Flags, defaultDir, (EAAPI.EnumFilenameDialog)Type);

            return result;
        }

        public string GetLastError()
        {
            string result = _apiProject.GetLastError();

            return result;
        }

        public string GetLink(string LinkGUID)
        {
            string result = _apiProject.GetLink(LinkGUID);

            return result;
        }

        public string GUIDtoXML(string GUID)
        {
            string result = _apiProject.GUIDtoXML(GUID);

            return result;
        }

        public bool ImportDirectory(string PackageGUID, string Language, string DirectoryPath, string ExtraOptions)
        {
            bool result = _apiProject.ImportDirectory(PackageGUID, Language, DirectoryPath, ExtraOptions);

            return result;
        }

        public bool ImportFile(string PackageGUID, string Language, string FileName, string ExtraOptions)
        {
            bool result = _apiProject.ImportFile(PackageGUID, Language, FileName, ExtraOptions);

            return result;
        }

        public string ImportPackageXMI(string PackageGUID, string FileName, int ImportDiagrams, int StripGUID)
        {
            string result = _apiProject.ImportPackageXMI(PackageGUID, FileName, ImportDiagrams, StripGUID);

            return result;
        }

        public bool IsValidating()
        {
            bool result = _apiProject.IsValidating();

            return result;
        }

        public bool LayoutDiagram(string DiagramGUID, int LayoutStyle)
        {
            bool result = _apiProject.LayoutDiagram(DiagramGUID, LayoutStyle);

            return result;
        }

        public bool LayoutDiagramEx(string DiagramGUID, int LayoutStyle, int Iterations, int LayerSpacing, int ColumnSpacing, bool SavetoDiagram)
        {
            bool result = _apiProject.LayoutDiagramEx(DiagramGUID, LayoutStyle, Iterations, LayerSpacing, ColumnSpacing, SavetoDiagram);

            return result;
        }

        public string LoadControlledPackage(string PackageGUID)
        {
            string result = _apiProject.LoadControlledPackage(PackageGUID);

            return result;
        }

        public bool LoadDiagram(string DiagramGUID)
        {
            bool result = _apiProject.LoadDiagram(DiagramGUID);

            return result;
        }

        public bool LoadProject(string FileName)
        {
            bool result = _apiProject.LoadProject(FileName);

            return result;
        }

        public void Migrate(string GUID, string SourceType, string DestType)
        {
            _apiProject.Migrate(GUID, SourceType, DestType);
        }

        public void MigrateToBPMN11(string GUID, string Type)
        {
            _apiProject.MigrateToBPMN11(GUID, Type);
        }

        public bool ProjectTransfer(string SourceFilePath, string TargetFilePath, string LogFilePath)
        {
            bool result = _apiProject.ProjectTransfer(SourceFilePath, TargetFilePath, LogFilePath);

            return result;
        }

        public bool PublishResult(string RuleID, EnumMVErrorType Severity, string ErrorMsg)
        {
            bool result = _apiProject.PublishResult(RuleID, (EAAPI.EnumMVErrorType)Severity, ErrorMsg);

            return result;
        }

        public bool PutDiagramImageOnClipboard(string DiagramGUID, int Type)
        {
            bool result = _apiProject.PutDiagramImageOnClipboard(DiagramGUID, Type);

            return result;
        }

        public bool PutDiagramImageToFile(string DiagramGUID, string FilePath, int Type)
        {
            bool result = _apiProject.PutDiagramImageToFile(DiagramGUID, FilePath, Type);

            return result;
        }

        public bool ReloadProject()
        {
            bool result = _apiProject.ReloadProject();

            return result;
        }

        public void RunHTMLReport(string PackageGUID, string ExportPath, string ImageFormat, string Style, string Extension)
        {
            _apiProject.RunHTMLReport(PackageGUID, ExportPath, ImageFormat, Style, Extension);
        }

        public void RunModelSearch(string QueryName, string SearchTerm, bool ShowInEA)
        {
            _apiProject.RunModelSearch(QueryName, SearchTerm, ShowInEA);
        }

        public void RunReport(string PackageGUID, string TemplateName, string FileName)
        {
            _apiProject.RunReport(PackageGUID, TemplateName, FileName);
        }

        public string SaveControlledPackage(string PackageGUID)
        {
            string result = _apiProject.SaveControlledPackage(PackageGUID);

            return result;
        }

        public string SaveDiagramImageToFile(string FileName)
        {
            string result = _apiProject.SaveDiagramImageToFile(FileName);

            return result;
        }

        public void ShowWindow(int Show)
        {
            _apiProject.ShowWindow(Show);
        }

        public bool SynchronizeClass(string ElementGUID, string ExtraOptions)
        {
            bool result = _apiProject.SynchronizeClass(ElementGUID, ExtraOptions);

            return result;
        }

        public bool SynchronizePackage(string PackageGUID, string ExtraOptions)
        {
            bool result = _apiProject.SynchronizePackage(PackageGUID, ExtraOptions);

            return result;
        }

        public bool TransformElement(string transformName, string ElementGUID, string TargetPackageGUID, string ExtraOptions)
        {
            bool result = _apiProject.TransformElement(transformName, ElementGUID, TargetPackageGUID, ExtraOptions);

            return result;
        }

        public bool TransformPackage(string transformName, string SourcePackageGUID, string TargetPackageGUID, string ExtraOptions)
        {
            bool result = _apiProject.TransformPackage(transformName, SourcePackageGUID, TargetPackageGUID, ExtraOptions);

            return result;
        }

        public bool ValidateDiagram(string DiagramGUID)
        {
            bool result = _apiProject.ValidateDiagram(DiagramGUID);

            return result;
        }

        public bool ValidateElement(string ElementGUID)
        {
            bool result = _apiProject.ValidateElement(ElementGUID);

            return result;
        }

        public bool ValidatePackage(string PackageGUID)
        {
            bool result = _apiProject.ValidatePackage(PackageGUID);

            return result;
        }

        public string XMLtoGUID(string GUID)
        {
            string result = _apiProject.XMLtoGUID(GUID);

            return result;
        }
    }
}
