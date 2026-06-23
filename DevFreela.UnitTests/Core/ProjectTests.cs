using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Core
{
    public class ProjectTests
    {
        [Fact]
        public void ProjectIsCreated_Start_Success()

        {
            // Arrange
            var project = new Project("Projeto A", "Descrição do projeto teste", 1, 2, 1000);

            // Act
            project.Start();
            // Assert
            
            Assert.Equal(ProjectStatusEnumm.InProgress, project.Status); // valor da esquerda é o valor esperado, valor da direita é o valor atual
            Assert.NotNull(project.StartedAt);

            Assert.True(project.Status == ProjectStatusEnumm.InProgress);
            

        }

        [Fact]
        public void ProjectIsInInvalidState_Complete_ThrowsException()
        {
            // Arrange
            var project = new Project("Projeto A", "Descrição do projeto teste", 1, 2, 1000);
            project.Start();
            // Act & Assert
            Action? start = project.Start;

            var exception = Assert.Throws<InvalidOperationException>(start!);

            Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);
        }
    }
}
