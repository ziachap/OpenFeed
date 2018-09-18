import * as React from "react";
import * as NewsReel from "../store/NewsReel";

type ArticleProps = NewsReel.Article

export default class ArticleCard extends React.Component<ArticleProps, {}> {

    constructor(props: ArticleProps) {
        super(props);
    }

    render() {

        return <div className="item">
                   <div className="image">
                       <img src={this.props.imageUrl} alt=""/>
                   </div>
                   <div className="content">
                       <a target="_blank" className="header" href={ this.props.url}>{this.props.title}</a>
                <div className="meta">
                    <span className="source">{this.props.source}</span> 
                    <span> - </span>
                           <span className="date">{this.props.publishDate}</span>
                       </div>
                       <div className="description">
                           <p>{this.props.description}</p>
                       </div>
				<div className="extra">
					<div className="ui label">{this.props.category}</div>
                    <div className="ui label">{this.props.source}</div>
                       </div>
                   </div>
               </div>;
    }

    /* // UI CARDS
    return <div className="column">
               <div className="ui card">
                   <div className="image">
                       <img src={this.props.imageUrl}/>
                   </div>
                   <div className="content">
                       <a className="header" href={this.props.url} target="_blank">{this.props.title}</a>
                       <div className="meta">
                    <span className="date">{this.props.publishDate}</span>
                       </div>
                       <div className="description">
                           {this.props.description}
                       </div>
                   </div>
                   <div className="extra content">
                       <a>
                           <i className="user icon"></i>
                           Something
                       </a>
                   </div>
               </div>
           </div>;
}*/
}