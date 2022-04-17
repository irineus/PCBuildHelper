using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Enums;
using PCBuildWeb.Services.Interfaces;

namespace PCBuildWeb.Utils.Filters
{
    public class BuildFilters
    {
        /// <summary>
        /// Filter a collection of parts with the given filter criteria.
        /// </summary>
        /// <typeparam name="T">Part Type</typeparam>
        /// <param name="collection">Collection of Parts</param>
        /// <param name="filter">Filter criteria</param>
        /// <returns>Colletcion of filtered parts</returns>
        public static ICollection<T> SimpleFilter<T>(ICollection<T> collection, Func<T, bool> filter)
        {
            return collection.Where(filter).ToList();
        }

        public static ICollection<T> MultipleFilter<T>(ICollection<T> collection, Func<T, bool> filter1, Func<T, bool> filter2)
        {
            return collection.Where(filter1).Where(filter2).ToList();
        }


        /// <summary>
        /// Filter a collection of parts only if there's at least one part with the given filter criteria. If not, just return the original collection.
        /// </summary>
        /// <typeparam name="T">Part Type</typeparam>
        /// <param name="collection">Collection of Parts</param>
        /// <param name="filter">Filter criteria</param>
        /// <returns>Colletcion of filtered parts (if there's at least one)</returns>
        public static ICollection<T> IfAnyFilter<T>(ICollection<T>? collection, Func<T, bool> filter)
        {
            if (collection is null)
            {
                return new List<T>();
            }
            if (collection.Where(filter).Any())
            {
                return collection.Where(filter).ToList();
            }
            return collection.ToList();
        }

        /// <summary>
        /// Search for the prerequisite part of a buid
        /// </summary>
        /// <param name="components">List of already selected components in the build</param>
        /// <param name="partPriority">Priority of the current part in the build</param>
        /// <param name="prerequisiteType">Type of the prerequisite component</param>
        /// <param name="serviceContext">Context service of the prerequisite componente</param>
        /// <returns></returns>
        public static async Task<T?> FindPrerequisitePartAsync<T>(List<Component>? components, PartType currentPartType, PartType prerequisiteType, IBuildPartService<T> serviceContext)
        {
            if (components is not null)
            {
                // Get both components
                Component? currentComponent = components.Where(c => c.PartType == currentPartType).FirstOrDefault();
                Component? preRequisiteComponent = components.Where(c => c.PartType == prerequisiteType).FirstOrDefault();
                // Both should have a value
                if ((currentComponent is not null) && (preRequisiteComponent is not null))
                {
                    // Only returns the prerequisite part if it's priority is higher (lower number) than the current part
                    if (preRequisiteComponent.Priority < currentComponent.Priority)
                    {
                        // Get the prerequisite part with the specific data for that part type
                        ComputerPart? buildPart = preRequisiteComponent.BuildPart;
                        if (buildPart is not null)
                        {
                            // Get the concrete prerequisite part from the database
                            T? selectedPart = await serviceContext.FindByIdAsync(buildPart.Id);
                            if (selectedPart is not null)
                            {
                                // Return the concrete part for the query
                                return selectedPart;
                            }
                        }
                    }
                }
            }
            return default;
        }

        public static async Task<T?> FindComponentPartAsync<T>(List<Component>? components, PartType componentType, IBuildPartService<T> serviceContext)
        {
            if (components is not null)
            {
                // Get both components
                Component? preRequisiteComponent = components.Where(c => c.PartType == componentType).FirstOrDefault();
                // Both should have a value
                if (preRequisiteComponent is not null)
                {
                    // Get the prerequisite part with the specific data for that part type
                    ComputerPart? buildPart = preRequisiteComponent.BuildPart;
                    if (buildPart is not null)
                    {
                        // Get the concrete prerequisite part from the database
                        T? selectedPart = await serviceContext.FindByIdAsync(buildPart.Id);
                        if (selectedPart is not null)
                        {
                            // Return the concrete part for the query
                            return selectedPart;
                        }
                    }

                }
            }
            return default;
        }
    }
}
