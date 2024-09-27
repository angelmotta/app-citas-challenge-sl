import React, { useState } from "react";
// import reactLogo from "./assets/react.svg";
// import viteLogo from "/vite.svg";
import "./App.css";

function App() {
    const [tipoDocumento, setTipoDocumento] = useState("DNI");
    const [numDocumento, setNumDocumento] = useState("");
    const [nombreCompleto, setNombreCompleto] = useState("");
    const [especialidad, setEspecialidad] = useState("medicina general");
    const [isFormOk, setIsFormOk] = useState(false);

    const handleSelectTipoDocumento = (
        e: React.ChangeEvent<HTMLSelectElement>
    ) => {
        setTipoDocumento(e.target.value);
    };

    const handleOnChangeNumDocumento = (
        e: React.ChangeEvent<HTMLInputElement>
    ) => {
        setNumDocumento(e.target.value);
    };

    const handleOnChangeNombreCompleto = (
        e: React.ChangeEvent<HTMLInputElement>
    ) => {
        setNombreCompleto(e.target.value);
    };

    const handleOnChangeEspecialidad = (
        e: React.ChangeEvent<HTMLSelectElement>
    ) => {
        setEspecialidad(e.target.value);
    };

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        // setIsFormOk(true);
        // Quick Form validation
        if (
            !tipoDocumento ||
            !numDocumento ||
            !nombreCompleto ||
            !especialidad
        ) {
            console.log("Formulario incompleto");
            alert("Formulario incompleto");
            return;
        }
        const requestCita = {
            tipoDocumento,
            numDocumento,
            nombreCompleto,
            especialidad,
            fechaHora: new Date().toISOString(),
        };
        console.log(`Request cita:`);
        console.log(requestCita);
    };

    return (
        <>
            <div>
                <h1>Registro citas</h1>
                <form onSubmit={handleSubmit}>
                    <div className="container-form">
                        <div>
                            <select
                                value={tipoDocumento}
                                onChange={handleSelectTipoDocumento}
                            >
                                <option value="DNI">DNI</option>
                                <option value="PASAPORTE">Pasaporte</option>
                            </select>
                            <input
                                type="text"
                                placeholder="Número de documento"
                                onChange={handleOnChangeNumDocumento}
                            />
                        </div>
                        <div>
                            <input
                                type="text"
                                placeholder="Nombre completo"
                                onChange={handleOnChangeNombreCompleto}
                            />
                        </div>
                        <div>
                            <select
                                value={especialidad}
                                onChange={handleOnChangeEspecialidad}
                            >
                                <option value="medicina general">
                                    Medicina general
                                </option>
                                <option value="pediatría">Pediatría</option>
                                <option value="cardiología">Cardiología</option>
                            </select>
                        </div>
                        <div>
                            <button type="submit">Crear cita</button>
                        </div>
                    </div>
                </form>
            </div>
            <div>
                <h2>Lista de citas</h2>
                <div>Content</div>
            </div>
        </>
    );
}

export default App;
