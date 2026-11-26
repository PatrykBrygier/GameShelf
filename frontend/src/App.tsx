import { type ReactNode } from "react";
import Navbar from "./components/Navbar/Navbar";

type AppProps = {
    children?: ReactNode;
};

function App({ children }: AppProps) {
    return (
        <div>
            <Navbar />
            {children}
        </div>
    );
}

export default App;
