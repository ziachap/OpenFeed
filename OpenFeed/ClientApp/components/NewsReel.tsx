import * as React from "react";
import { RouteComponentProps } from "react-router-dom";
import { connect } from 'react-redux';
import * as NewsReelState from "../store/NewsReel";
import { ApplicationState } from '../store';
import ArticleCard from "./ArticleCard";
import { newsCategories, newsCategoryService} from "../models/NewsCategories";
import "jquery";

type NewsReelProps = NewsReelState.NewsReelState
    & typeof NewsReelState.actionCreators
    & RouteComponentProps<{}>;

class NewsReel extends React.Component<NewsReelProps, {}> {

	constructor(props: NewsReelProps) {
		super(props);
		this.setCategoryHandler = this.setCategoryHandler.bind(this);
		this.renderCategoryInput = this.renderCategoryInput.bind(this);
	}

    componentWillMount() {
	    this.requestArticles();
    }

    componentWillReceiveProps(nextProps: NewsReelProps) {
	}

	private requestArticles(): void {
		this.props.requestArticles(this.props.searchConfiguration);
	}

	public setCategoryHandler(event: any, category?: number): void {
		this.props.setCategory(category);
		this.requestArticles();
	}

    public render() {
        return <div className="ui container text">

                   <div className="ui very padded segment">
                       <div className="ui right rail">
                           {this.renderMenu()}
                       </div>
				<h1>United Kingdom > {newsCategoryService.getName(this.props.searchConfiguration.categoryId)}</h1>
                       <p>This is the news!</p>
                       {this.props.isLoading ? <span>Loading...</span> : this.renderArticles()}
                   </div>
               </div>;
	}

    private renderArticles() {
        return <div className="ui divided items">
                   {this.props.articles.map(article => <ArticleCard {...article} key={article.title}/>)}
               </div>;
    }
    
	private renderMenu() {
        return <div className="ui">
			<div className="ui vertical text small menu">

                <div className="header item">Sort By</div>
                <a className="item">Most Relevant</a>
                <a className="item">Most Popular</a>
				<a className="active item">Newest</a>

				<div className="header item">Categories</div>

				{this.renderCategoryInput("All", undefined)}
				{newsCategories.map(category => this.renderCategoryInput(category.name, category.id))}

                <div className="header item">Other</div>
                <div className="ui small form">
                    <div className="inline field">
                        <div className="ui checkbox">
                            <input type="checkbox" className="hidden" />
                            <label>Business</label>
                        </div>
                    </div>
                </div>
               </div>
           </div>;
	}

	private renderCategoryInput(text: string, id?: number) {
		return <a className={"item " + (this.props.searchConfiguration.categoryId === id ? "active" : "")}
			onClick={(e: any) => this.setCategoryHandler(e, id)}>{text}</a>
	}

}

/*
private renderPagination() {
        let prevStartDateIndex = (this.props.startDateIndex || 0) - 5;
        let nextStartDateIndex = (this.props.startDateIndex || 0) + 5;

        return <p className='clearfix text-center'>
            <Link className='btn btn-default pull-left' to={`/fetchdata/${prevStartDateIndex}`}>Previous</Link>
            <Link className='btn btn-default pull-right' to={`/fetchdata/${nextStartDateIndex}`}>Next</Link>
            {this.props.isLoading ? <span>Loading...</span> : []}
        </p>;
    }*/


export default connect(
(state: ApplicationState) => state.newsReel, // Selects which state properties are merged into the component's props
NewsReelState.actionCreators // Selects which action creators are merged into the component's props
)(NewsReel) as typeof NewsReel

