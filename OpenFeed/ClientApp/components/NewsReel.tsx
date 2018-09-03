import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as NewsReelState from '../store/NewsReel';
import NewsService from '../services/NewsService';
import Article from '../services/NewsService';

type NewsReelProps = NewsReelState.NewsReelState
    & typeof NewsReelState.actionCreators
    & RouteComponentProps<{}>;

class NewsReel extends React.Component<NewsReelProps, {}> {
    componentWillMount() {
        this.props.requestArticles();
    }

    componentWillReceiveProps(nextProps: NewsReelProps) {
        //this.props.requestArticles();
    }

    public render() {
        return <div>
            <h1>The News</h1>
            <p>This is the news!</p>
            {this.props.isLoading ? <span>Loading...</span> : []}
            {this.renderArticles()}
        </div>;
    }

    private renderArticles() {
        return <table className='table'>
            <thead>
                <tr>
                    <th>Title</th>
                </tr>
            </thead>
            <tbody>
                {this.props.articles.map(article =>
                    <tr>
                        <td key={article.title}>
                            <img src={article.imageUrl} width="200px" />
                            <a target="_blank" href={article.url}><b> {article.title}</b></a>
                            <div dangerouslySetInnerHTML={{ __html: article.description }} />
                        </td>
                    </tr>
                )}
            </tbody>
        </table>;
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
}

export default connect(
    (state: ApplicationState) => state.newsReel, // Selects which state properties are merged into the component's props
    NewsReelState.actionCreators                 // Selects which action creators are merged into the component's props
)(NewsReel) as typeof NewsReel;
