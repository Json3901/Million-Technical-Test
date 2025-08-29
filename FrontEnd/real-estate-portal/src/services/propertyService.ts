import {baseUrl} from "../config/apiConfig";

let service = "Property";

export async function getProperties(filters: any, page: number, pageSize: number) {
    const body = {
        Names: filters.name ? [filters.name] : [],
        OwnerNames: filters.ownerName ? [filters.ownerName] : [],
        Addresses: filters.address ? [filters.address] : [],
        MinPrice: filters.minPrice ?? 0,
        MaxPrice: filters.maxPrice ?? 100000000,
        InternalCodes: filters.internalCode ? [filters.internalCode] : [],
        Years: filters.year ? [filters.year] : [],
        PageSize: pageSize,
        PageNumber: page,
        SortBy: filters.SortBy ?? "CreatedAt",
        OrderDesc: filters.OrderDesc ?? true,
    };

    const response = await fetch(`${baseUrl}/${service}/search`, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(body),
    });

    if (!response.ok) throw new Error("Error al obtener propiedades");
    return await response.json();
}

export async function getPropertyDetail(id: string) {
    const response = await fetch(`${baseUrl}/${service}/${id}`, {
        method: "GET",
        headers: {"Content-Type": "application/json"},
    });

    if (!response.ok) throw new Error("Error al obtener detalle de la propiedad");
    return await response.json();
}
