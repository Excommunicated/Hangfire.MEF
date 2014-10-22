using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;

namespace HangFire.MEF
{
    public static class MEFExtensions
    {
        public static object GetExportedValueByType(this CompositionContainer container, Type type)
        {
            if (Enumerable.Any(container.Catalog.Parts.SelectMany(partDef => partDef.ExportDefinitions), exportDef => exportDef.ContractName == type.FullName))
            {
                var contract = AttributedModelServices.GetContractName(type);
                var definition = new ContractBasedImportDefinition(contract, contract, null, ImportCardinality.ExactlyOne,
                    false, false, CreationPolicy.Any);
                return container.GetExports(definition).FirstOrDefault().Value;
            }

            return null;
        }
    }
}