import * as React from "react";

export class NavMenu extends React.Component<{}, {}> {
    render() {
        return <div className="ui fixed menu">
                   <div className="ui container">
                       <a href="#" className="header item">
                           <img className="logo" src="images/of_logo_dark.png" alt="OpenFeed"/>
                           OpenFeed
                       </a>
                       <a className="item" href={"/newsreel"}>News Reel</a>
                       <div className="ui simple dropdown item">
                           Categories <i className="dropdown icon"></i>
                           <div className="menu">
                               <a className="item" href="/">World</a>
                               <a className="item" href="#">Technology</a>
                               <div className="divider"></div>
                               <div className="header">Header Item</div>
                               <div className="item">
                                   <i className="dropdown icon"></i>
                                   Sub Menu
                                   <div className="menu">
                                       <a className="item" href="#">Link Item</a>
                                       <a className="item" href="#">Link Item</a>
                                   </div>
                               </div>
                               <a className="item" href="#">Link Item</a>
                           </div>
                       </div>
                       <div className="right item">
                           <div className="ui input">
                               <input type="text" placeholder="Search..." />
                           </div>
                       </div>
                   </div>
               </div>;
    }
}