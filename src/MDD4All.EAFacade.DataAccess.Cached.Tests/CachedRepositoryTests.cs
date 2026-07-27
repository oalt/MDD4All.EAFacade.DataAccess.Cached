using MDD4All.EAFacade.DataModels.Contracts;
using System.Linq;
using Xunit;

namespace MDD4All.EAFacade.DataAccess.Cached.Tests
{
    [Collection(EaDatabaseCollection.Name)]
    public class CachedRepositoryTests
    {
        private readonly EaRepositoryFixture _repositoryFixture;

        public CachedRepositoryTests(EaRepositoryFixture repositoryFixture)
        {
            _repositoryFixture = repositoryFixture;
        }

        private Package GetRootPackage()
        {
            CachedRepository cachedRepository = new CachedRepository(_repositoryFixture.ApiRepository);

            cachedRepository.CacheAll();

            Package result = (Package)cachedRepository.Models.GetAt(0);

            return result;
        }

        private Package FindOrCreatePackage1(Package rootPackage)
        {
            Package result;

            GenericCollection<Package> childPackages = (GenericCollection<Package>)rootPackage.Packages;

            Package existingPackage1 = childPackages.FirstOrDefault(package => package.Name == "Package1");

            if (existingPackage1 != null)
            {
                result = existingPackage1;
            }
            else
            {
                result = (Package)rootPackage.Packages.AddNew("Package1", "Nothing");
            }

            return result;
        }

        private Element FindOrCreateElement(Package package, string name, string type)
        {
            Element result;

            GenericCollection<Element> packageElements = (GenericCollection<Element>)package.Elements;

            Element existingElement = packageElements.FirstOrDefault(element => element.Name == name && element.Type == type);

            if (existingElement != null)
            {
                result = existingElement;
            }
            else
            {
                result = (Element)package.Elements.AddNew(name, type);
            }

            return result;
        }

        private Diagram FindOrCreateDiagram(Package package, string name, string type)
        {
            Diagram result;

            GenericCollection<Diagram> packageDiagrams = (GenericCollection<Diagram>)package.Diagrams;

            Diagram existingDiagram = packageDiagrams.FirstOrDefault(diagram => diagram.Name == name && diagram.Type == type);

            if (existingDiagram != null)
            {
                result = existingDiagram;
            }
            else
            {
                result = (Diagram)package.Diagrams.AddNew(name, type);
            }

            return result;
        }

        private void PlaceElementOnDiagram(Diagram diagram, Element element, int leftOffset, int topOffset)
        {
            const int elementWidth = 200;
            const int elementHeight = 100;

            DiagramObject diagramObject = (DiagramObject)diagram.DiagramObjects.AddNew("", "");

            diagramObject.ElementID = element.ElementID;

            // Enterprise Architect's diagram coordinate origin (0, 0) is the top-left corner;
            // left/right are always positive (growing rightward), top/bottom are always
            // negative (growing more negative further down the diagram).
            diagramObject.left = leftOffset;
            diagramObject.right = leftOffset + elementWidth;
            diagramObject.top = topOffset;
            diagramObject.bottom = topOffset - elementHeight;

            diagramObject.Update();
        }

        [Fact]
        public void AddChildPackage_UnderRootModel_CreatesNewPackage()
        {
            Package rootPackage = GetRootPackage();

            Package package1 = FindOrCreatePackage1(rootPackage);

            Assert.Equal("Package1", package1.Name);

            GenericCollection<Package> childPackages = (GenericCollection<Package>)rootPackage.Packages;

            Assert.Contains(childPackages, package => package.Name == "Package1");
        }

        [Fact]
        public void AddElements_UnderPackage1_CreatesThreeClasses()
        {
            Package rootPackage = GetRootPackage();

            Package package1 = FindOrCreatePackage1(rootPackage);

            Element ticElement = FindOrCreateElement(package1, "Tic", "Class");
            Element tacElement = FindOrCreateElement(package1, "Tac", "Class");
            Element toeElement = FindOrCreateElement(package1, "Toe", "Class");

            Assert.Equal("Tic", ticElement.Name);
            Assert.Equal("Class", ticElement.Type);
            Assert.Equal("Tac", tacElement.Name);
            Assert.Equal("Class", tacElement.Type);
            Assert.Equal("Toe", toeElement.Name);
            Assert.Equal("Class", toeElement.Type);

            GenericCollection<Element> packageElements = (GenericCollection<Element>)package1.Elements;

            Assert.Equal(3, packageElements.Count);
            Assert.Contains(packageElements, element => element.Name == "Tic" && element.Type == "Class");
            Assert.Contains(packageElements, element => element.Name == "Tac" && element.Type == "Class");
            Assert.Contains(packageElements, element => element.Name == "Toe" && element.Type == "Class");
        }

        [Fact]
        public void AddDiagram_UnderPackage1_PlacesThreeClassesAsDiagramObjects()
        {
            Package rootPackage = GetRootPackage();

            Package package1 = FindOrCreatePackage1(rootPackage);

            Element ticElement = FindOrCreateElement(package1, "Tic", "Class");
            Element tacElement = FindOrCreateElement(package1, "Tac", "Class");
            Element toeElement = FindOrCreateElement(package1, "Toe", "Class");

            Diagram diagram = FindOrCreateDiagram(package1, "All classes", "Logical");

            Assert.Equal("All classes", diagram.Name);
            Assert.Equal("Logical", diagram.Type);

            PlaceElementOnDiagram(diagram, ticElement, leftOffset: 50, topOffset: -50);
            PlaceElementOnDiagram(diagram, tacElement, leftOffset: 300, topOffset: -50);
            PlaceElementOnDiagram(diagram, toeElement, leftOffset: 550, topOffset: -50);

            GenericCollection<DiagramObject> diagramObjects = diagram.DiagramObjects;

            Assert.Equal(3, diagramObjects.Count);
            Assert.Contains(diagramObjects, diagramObject => diagramObject.ElementID == ticElement.ElementID);
            Assert.Contains(diagramObjects, diagramObject => diagramObject.ElementID == tacElement.ElementID);
            Assert.Contains(diagramObjects, diagramObject => diagramObject.ElementID == toeElement.ElementID);

            DiagramObject ticDiagramObject = diagramObjects.First(diagramObject => diagramObject.ElementID == ticElement.ElementID);

            Assert.True(ticDiagramObject.left < ticDiagramObject.right);
            Assert.True(ticDiagramObject.top > ticDiagramObject.bottom);
            Assert.True(ticDiagramObject.left >= 0);
            Assert.True(ticDiagramObject.right >= 0);
            Assert.True(ticDiagramObject.top < 0);
            Assert.True(ticDiagramObject.bottom < 0);
        }
    }
}
