import { useState, useEffect } from "react";
class ApiError extends Error {
    code: number;
    constructor(message: string, code: number) {
        super(message);
        this.code = code;
    }
}

const RequestCitaForm = (props: any) => {
    const { fetchCitas } = props;
    const [especialidades, setEspecialidades] = useState([]);
    const REST_API_CITAS = "http://localhost:5118/citas";
    const REST_API_ESPECIALIDADES = "http://localhost:5118/especialidades";
    const [tipoDocumento, setTipoDocumento] = useState("DNI");
    const [numDocumento, setNumDocumento] = useState("");
    const [nombreCompleto, setNombreCompleto] = useState("");
    const [especialidad, setEspecialidad] = useState("medicina general");

    useEffect(() => {
        console.log(`called useEffect RequestCitaForm: get especialidades`);

        fetchEspecialidades();
    }, []);

    const fetchEspecialidades = async () => {
        console.log(`Fetching especialidades...`);
        const response = await fetch(REST_API_ESPECIALIDADES);
        // check if response is 200
        if (!response.ok) {
            // handle error
            console.error("Error fetching especialidades");
            alert("Servicio Citas no disponible (Especialidades)");
            return;
        }
        const data = await response.json();
        console.log(data);
        setEspecialidades(data);
    };

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

        // Form is valid
        // Save a reference to the form element before execute http request in async function
        const formReference = e.currentTarget;
        const requestCita = {
            docIdType: tipoDocumento,
            numDocId: numDocumento,
            fullName: nombreCompleto,
            specialtyId: especialidad,
        };
        console.log(`Request cita:`);
        console.log(requestCita);

        // Send requestCita to backend API
        const sendRequest = async () => {
            try {
                const response = await fetch(REST_API_CITAS, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify(requestCita),
                });

                // check if response is HTTP status 200
                if (!response.ok) {
                    console.log("Error al crear la cita");
                    throw new ApiError(
                        "Error al crear la cita",
                        response.status
                    );
                }
                // Clean state
                setTipoDocumento("DNI");
                setNumDocumento("");
                setNombreCompleto("");
                setEspecialidad("medicina general");
                // Clean data from form html elements
                formReference.reset();

                // Update list of citas
                fetchCitas();
                alert("Cita creada correctamente");
            } catch (err) {
                // custom error handling
                if (err instanceof ApiError) {
                    alert("Error al crear la cita");
                }
                alert("Servicio de citas no disponible");
            }
        };

        sendRequest();
    };
    return (
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
                        {/* <option value="medicina general">
                            Medicina general
                        </option>
                        <option value="pediatría">Pediatría</option>
                        <option value="cardiología">Cardiología</option> */}
                        {especialidades.map((especialidad: any) => (
                            <option
                                key={especialidad.id}
                                value={especialidad.id}
                            >
                                {especialidad.name}
                            </option>
                        ))}
                    </select>
                </div>
                <div>
                    <button type="submit">Crear cita</button>
                </div>
            </div>
        </form>
    );
};

export default RequestCitaForm;
