export function formatPrice(value: number): string {
    return value.toLocaleString("es-ES", {
        style: "currency",
        currency: "USD",
        minimumFractionDigits: 0,
        maximumFractionDigits: 0,
    });
}
