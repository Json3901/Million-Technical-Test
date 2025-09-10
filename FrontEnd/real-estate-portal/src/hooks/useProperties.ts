import { useState, useEffect } from "react";
import { getProperties } from "../services/propertyService";

export function useProperties(filters: PropertyFilters, page = 1, pageSize = 10) {
    const [data, setData] = useState({
        items: [],
        count: 0,
        pageNumber: page,
        pageSize: pageSize,
    });

    useEffect(() => {
        getProperties(filters, page, pageSize)
            .then((response) => setData(response))
            .catch(() => setData({
                items: [],
                count: 0,
                pageNumber: page,
                pageSize: pageSize,
            }));
    }, [filters, page, pageSize]);

    return data;
}
