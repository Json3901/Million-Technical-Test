export interface PropertyTrace {
    name: string;
    value: number;
    tax: number;
    dateSale: string;
}

export interface PropertyDetail {
    id: string;
    name: string;
    address: string;
    price: number;
    owner: string;
    year: number;
    internalCode: string;
    images: PropertyImage[];
    traces: PropertyTrace[];
}

export interface PropertyImage {
    urlFile: string;
}
