﻿using System.ComponentModel; 
using ContosoSuitesWebAPI.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.SemanticKernel;

namespace ContosoSuitesWebAPI.Plugins
{
    /// <summary>
    /// The maintenance request plugin for creating and saving maintenance requests.
    /// </summary>
    public class MaintenanceRequestPlugin(CosmosClient cosmosClient)
    {
        private readonly CosmosClient _cosmosClient = cosmosClient;

        // Exercise 5 Task 2 TODO #6: Add KernelFunction and Description descriptors to the function.
        // The function should be named "create_maintenance_request" and it should have a description
        // the accurately describes the purpose of the function, such as "Creates a new maintenance request for a hotel."

        
        /// <summary>
        /// Creates a new maintenance request for a hotel.
        /// </summary>
        [KernelFunction("create_maintenance_request")]
        [Description("Creates a new maintenance request for a hotel.")]
        public async Task<MaintenanceRequest> CreateMaintenanceRequest(Kernel kernel, int HotelId, string Hotel, string Details, int? RoomNumber,string? location)
        {
            try
            {
                Console.WriteLine($"Creating a new maintenance request for the {Hotel}.");

                var request = new MaintenanceRequest
                {
                    id = Guid.NewGuid().ToString(),
                    hotel_id = HotelId,
                    hotel = Hotel,
                    details = Details,
                    room_number = RoomNumber,
                    source = "customer",
                    location = location
                };
                return request;
            }
            catch (Exception ex)
            {
                throw new Exception($"An exception occurred while generating a new maintenance request: {ex}");
            }
        }

        // Exercise 5 Task 2 TODO #8: Add KernelFunction and Description descriptors to the function.
        // The function should be named "save_maintenance_request" and it should have a description
        // the accurately describes the purpose of the function, such as "Saves a maintenance request to the database for a hotel."

        
        /// <summary>
        /// Saves a maintenance request to the database for a hotel.
        /// </summary>
        [KernelFunction("save_maintenance_request")]
        [Description("Saves a maintenance request to the database for a hotel.")]
        public async Task SaveMaintenanceRequest(Kernel kernel,MaintenanceRequest maintenanceRequest)
        {
            var db = _cosmosClient.GetDatabase("ContosoSuites");
            var container = db.GetContainer("MaintenanceRequests");

            var response = await container.CreateItemAsync(maintenanceRequest, new PartitionKey(maintenanceRequest.hotel_id));
        }
    }
}
