import React, { useEffect, useState } from 'react';
import style from '../Tools/Style';
import { Button, Container, TextField, Typography, Grid } from '@material-ui/core';
import { obtenerUrusarioActual, actualizarUsuario } from '../../actions/UsuarioAction';

const PerfilUsuario = () => {
    const [usuario, setUsuario] = useState({
        nombreCompleto: '',
        username: '',
        email: '',
        password: '',
        confirmarPassword: ''
    });

    const ingresarValoresMemoria = valores => {
        const { name, value } = valores.target;
        setUsuario(anterior => ({
            ...anterior,
            [name]: value
        }));
    }

    useEffect(() => {
        obtenerUrusarioActual().then(response => {
            console.log('esta es la data del objeto response del usuairo actual', response);
            setUsuario(response.data);
        });
    }, []);

    const guardarUsuario = valores => {
        valores.preventDefault();
        actualizarUsuario(usuario).then(response => {
            console.log('se actualizo correctamente el usuario ', usuario);
            window.localStorage.setItem('token_seguridad', response.data.token);
        });
    }

    return (
        <Container>
            <div style={style.paper}>
                <Typography component="h1" variants="h5">
                    Perfil de Usuario
                </Typography>
            </div>

            <form style={style.form}>
                <Grid container spacing={2}>
                    <Grid item xs={12} md={6}>
                        <TextField name="NombreCompleto" value={usuario.nombreCompleto} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese nombre y apellidos" />
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name="Username" value={usuario.username} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese username" />
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name="Email" value={usuario.email} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese email" />
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name="Password" value={usuario.password} onChange={ingresarValoresMemoria} type="password" variant="outlined" fullWidth label="Ingrese password" />
                    </Grid>
                    <Grid item xs={12} md={6}>
                        <TextField name="ConfirmePassword" value={usuario.confirmarPassword} onChange={ingresarValoresMemoria} type="password" variant="outlined" fullWidth label="Confirme password" />
                    </Grid>
                </Grid>
                <Grid container justify="center">
                    <Grid item xs={12} md={6}>
                        <Button type="submit" onClick={guardarUsuario} fullWidth variant="contained" size="large" color="primary" style={style.submit}>
                            Guardar Datos
                        </Button>
                    </Grid>
                </Grid>
            </form>
        </Container>
    );
}

export default PerfilUsuario;