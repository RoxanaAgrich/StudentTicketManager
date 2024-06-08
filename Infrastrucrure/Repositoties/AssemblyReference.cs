using System.Reflection;

namespace Infrastrucrure.Repositoties
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
