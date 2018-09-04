import * as React from "react";
import { NavMenu } from "./NavMenu";
import { Footer } from "./Footer";

export class Layout extends React.Component<{}, {}> {

    render() {
        return <div className="">
                   <NavMenu/>
                   <div className="ui main container">
                       { this.props.children }
                   </div>
                   <Footer/>
               </div>;
    }

}