namespace BlazorWasmTemplate.Application.Constants;

public class ApiEndpoint
{
    /// <summary>
    /// Identity Module
    /// </summary>
    public const string SignIn = "/api/identity/sign-in";
    public const string Refresh = "/api/identity/refresh";

    /// <summary>
    /// Region Endpoint API
    /// </summary>
    public const string GetAllProvince = "/api/region/provinces";
    public const string GetAllRegency = "/api/region/regencies";
    public const string GetAllDistrict = "/api/region/districts";
    public const string GetAllVillage = "/api/region/villages";

    /// <summary>
    /// User Endpoint API
    /// </summary>
    public const string GetAllUsers = "/api/user-management/users";
    public const string GetUserById = "/api/user-management/users/";
    public const string GetAllUserRoles = "/api/user-management/users/roles";
    public const string CreateUser = "/api/user-management/users";
    public const string UpdateUser = "/api/user-management/users/";

    /// <summary>
    /// Water Supply Endpoint API
    /// </summary>
    public const string GetAllWaterSupply = "/api/water-supply-management/water-supplies";
    public const string GetWaterSupplyById = "/api/water-supply-management/water-supplies/";
    public const string CreateWaterSupply = "/api/water-supply-management/water-supplies"; 
    public const string GetAllWaterSupplyType = "/api/water-supply-management/water-supplies/types"; 
    public const string GetAllWaterSupplyCondition = "/api/water-supply-management/water-supplies/conditions"; 
    public const string GetAllWaterSupplyInfrastructure = "/api/water-supply-management/water-supplies/infrastructures";

    /// <summary>
    /// GIS API Endpoint
    /// </summary>
    public const string GetAllWaterSupplyCoordinate = "/api/gis/water-supplies";
    public const string GetDetailWaterSupply = "/api/gis/water-supplies/";
}

