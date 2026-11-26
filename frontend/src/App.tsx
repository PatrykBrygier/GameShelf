import { type ReactNode } from "react";
import Navbar from "./components/Navbar/Navbar";

type AppProps = {
    Children: ReactNode;
};

function App({ Children }: AppProps) {
    return (
        <div>
            <Navbar />
            {Children}
        </div>
    );
}

export default App;
