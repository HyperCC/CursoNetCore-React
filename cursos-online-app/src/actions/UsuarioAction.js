import HttpClient from '../servicios/HttpCliente';

export const registrarUsuario = usuario => {
    return new Promise((resolve, eject) => {
        HttpClient.post('/usuario/registrar', usuario).then(response => {
            resolve(response);
        });
    });
}

export const obtenerUsuarioActual = () => {
    return new Promise((resolve, eject) => {
        HttpClient.get('/usuario').then(response => {
            dispatch({
                type: "INICIAR_SESION",
                sesion: response.data,
                autenticado: true
            });
            resolve(response);
        });
    });
}

export const actualizarUsuario = (usuario) => {
    return new Promise((resolve, eject) => {
        HttpClient.put('/usuario', usuario).then(response => {
            resolve(response);
        });
    });
}

export const loginUsuario = usuario => {
    return new Promise((resolve, eject) => {
        HttpClient.post("/usuario/login", usuario).then(response => {
            resolve(response);
        });
    });
}

