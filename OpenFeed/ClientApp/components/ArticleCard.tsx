import * as React from "react";

type ArticleCardProps = {
    title: string;
    description: string;
    url: string;
    imageUrl: string;
}

export default class ArticleCard extends React.Component<ArticleCardProps, {}> {

    constructor(props: ArticleCardProps) {
        super(props);
        console.log(props);
    }

    componentWillMount() {
    }

    componentWillReceiveProps(nextProps: ArticleCardProps) {
    }

    render() {
        return <div className="articlecard">
            <div>
                <img className="img-fluid" src={this.props.imageUrl} alt="" width="500"/>
                <div className="articlecard_content">
                    <a href={this.props.url}>
                        <h3>{this.props.title}</h3>
                    </a>
                    <div dangerouslySetInnerHTML={{ __html: this.props.description }} />
                </div>
            </div>
                   <div className="ui card"><div className="image">
                           <img src="/images/avatar2/large/kristy.png">
                       </div>
                       <div className="content">
                           <a className="header">Kristy</a>
                           <div className="meta">
                               <span className="date">Joined in 2013</span>
                           </div>
                           <div className="description">
                               Kristy is an art director living in New York.
                           </div>
                       </div>
                       <div className="extra content">
                           <a>
                               <i className="user icon"></i>
                               22 Friends
                           </a>
                       </div>
                   </div>
        </div> ;
    }
}