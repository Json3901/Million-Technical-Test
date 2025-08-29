import React, { useEffect, useState } from "react";
import { Modal, Box, Typography, CircularProgress, Grid, Divider } from "@mui/material";
import { getPropertyDetail } from "../services/propertyService";
import { ImageCarousel } from "./ImageCarousel";

type Props = {
    propertyId: string | null;
    open: boolean;
    onClose: () => void;
};

export const PropertyDetailModal: React.FC<Props> = ({ propertyId, open, onClose }) => {
    const [loading, setLoading] = useState(false);
    const [detail, setDetail] = useState<any>(null);

    useEffect(() => {
        if (open && propertyId) {
            setLoading(true);
            getPropertyDetail(propertyId)
                .then((data) => setDetail(data))
                .catch(() => setDetail(null))
                .finally(() => setLoading(false));
        }
    }, [open, propertyId]);

    return (
        <Modal open={open} onClose={onClose}>
            <Box sx={{
                bgcolor: "background.paper",
                p: { xs: 2, sm: 4 },
                maxWidth: 700,
                mx: "auto",
                mt: { xs: "5vh", sm: "10vh" },
                borderRadius: 2,
                boxShadow: 24,
                width: { xs: "95%", sm: "80%" }
            }}>
                {loading ? (
                    <Box sx={{ display: "flex", justifyContent: "center", alignItems: "center", minHeight: 200 }}>
                        <CircularProgress />
                    </Box>
                ) : detail ? (
                    <Grid container spacing={3} justifyContent="center" alignItems="center">
                        <Grid item xs={12} md={6} sx={{ display: "flex", justifyContent: { xs: "center", md: "flex-start" } }}>
                            <ImageCarousel images={detail.images || []} />
                        </Grid>
                        <Grid item xs={12} md={6} sx={{ textAlign: { xs: "center", md: "left" } }}>
                            <Typography variant="h6" gutterBottom>{detail.name}</Typography>
                            <Typography variant="body2" color="text.secondary" gutterBottom>
                                <b>Dirección:</b> {detail.address}
                            </Typography>
                            <Typography variant="body2" color="text.secondary" gutterBottom>
                                <b>Precio:</b> ${detail.price}
                            </Typography>
                            <Typography variant="body2" color="text.secondary" gutterBottom>
                                <b>Propietario:</b> {detail.owner}
                            </Typography>
                            <Typography variant="body2" color="text.secondary" gutterBottom>
                                <b>Año:</b> {detail.year}
                            </Typography>
                            <Typography variant="body2" color="text.secondary" gutterBottom>
                                <b>Código Interno:</b> {detail.internalCode}
                            </Typography>
                        </Grid>
                        <Grid item xs={12}>
                            <Divider sx={{ my: 2 }} />
                            <Typography variant="subtitle1" gutterBottom textAlign={{ xs: "center", md: "left" }}>
                                Historial de ventas:
                            </Typography>
                            <Grid container spacing={2} justifyContent="center">
                                {detail.traces?.map((trace: any, idx: number) => (
                                    <Grid item xs={12} sm={6} md={4} key={idx}>
                                        <Box sx={{
                                            bgcolor: "grey.100",
                                            p: 2,
                                            borderRadius: 2,
                                            mb: 1,
                                            boxShadow: 1,
                                            textAlign: { xs: "center", md: "left" }
                                        }}>
                                            <Typography variant="body2"><b>Nombre:</b> {trace.name}</Typography>
                                            <Typography variant="body2"><b>Valor:</b> ${trace.value}</Typography>
                                            <Typography variant="body2"><b>Impuesto:</b> ${trace.tax}</Typography>
                                            <Typography variant="body2"><b>Fecha de venta:</b> {new Date(trace.dateSale).toLocaleDateString()}</Typography>
                                        </Box>
                                    </Grid>
                                ))}
                            </Grid>
                        </Grid>
                    </Grid>
                ) : (
                    <Typography>No hay información disponible.</Typography>
                )}
            </Box>
        </Modal>
    );
}
